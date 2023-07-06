﻿using System;
using System.Collections.Generic;
using System.Linq;
using NMG.Core.Domain;
using MySql.Data.MySqlClient;
using System.Data;

namespace NMG.Core.Reader
{
    public class MysqlMetadataReader : IMetadataReader
    {
        private readonly string connectionStr;

        public MysqlMetadataReader(string connectionStr)
        {
            this.connectionStr = connectionStr;
        }


        #region IMetadataReader Members

        public IList<Column> GetTableDetails(Table table, string owner)
        {
            var columns = new List<Column>();
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (MySqlCommand tableDetailsCommand = conn.CreateCommand())
                    {
                        tableDetailsCommand.CommandText = string.Format(@"DESCRIBE {0}.{1}", owner, table);
                        using (MySqlDataReader sqlDataReader = tableDetailsCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            while (sqlDataReader.Read())
                            {
                                string columnName = sqlDataReader.GetString(0);
                                string dataType = sqlDataReader.GetString(1);
                                bool isNullable = sqlDataReader.GetString(2).Equals("YES",
                                                                                    StringComparison.
                                                                                        CurrentCultureIgnoreCase);
                                bool isPrimaryKey =
                                    (!sqlDataReader.IsDBNull(3)
                                         ? sqlDataReader.GetString(3).Equals(
                                             MysqlConstraintType.PrimaryKey.ToString(),
                                             StringComparison.CurrentCultureIgnoreCase)
                                         : false);
                                bool isForeignKey =
                                    (!sqlDataReader.IsDBNull(3)
                                         ? sqlDataReader.GetString(3).Equals(
                                             MysqlConstraintType.ForeignKey.ToString(),
                                             StringComparison.CurrentCultureIgnoreCase)
                                         : false);

                                var m = new DataTypeMapper();

                                columns.Add(new Column
                                {
                                    Name = columnName,
                                    DataType = dataType,
                                    IsNullable = isNullable,
                                    IsPrimaryKey = isPrimaryKey,
                                    //IsPrimaryKey(selectedTableName.Name, columnName)
                                    IsForeignKey = isForeignKey,
                                    // IsFK()
                                    MappedDataType =
                                        m.MapFromDBType(ServerType.MySQL, dataType, null, null, null).ToString(),
                                    //DataLength = dataLength
                                });

                                table.Columns = columns;
                            }
                            table.Owner = owner;
                            table.PrimaryKey = DeterminePrimaryKeys(table);

                            // Need to find the table name associated with the FK
                            foreach (var c in table.Columns)
                            {
                                c.ForeignKeyTableName = GetForeignKeyReferenceTableName(table.Name, c.Name);
                            }
                            table.ForeignKeys = DetermineForeignKeyReferences(table);
                            table.HasManyRelationships = DetermineHasManyRelationships(table);
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            
            return columns;
        }

        public IList<string> GetOwners()
        {
            var owners = new List<string>();
            var conn = new MySqlConnection(connectionStr);
            
            conn.Open();
            try
            {
                using (conn)
                {
                    var tableCommand = conn.CreateCommand();
                    tableCommand.CommandText = @"select distinct table_schema from information_schema.tables
                                                union
                                                select schema_name from information_schema.schemata
                                                ";
                    var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        var ownerName = sqlDataReader.GetString(0);
                        owners.Add(ownerName);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            return owners;
        }

        public List<Table> GetTables(string owner)
        {
            var tables = new List<Table>();
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            try
            {
                using (conn)
                {
                    var tableCommand = conn.CreateCommand();
                    tableCommand.CommandText = String.Format("select table_name from information_schema.tables where table_type like 'BASE TABLE' and TABLE_SCHEMA = '{0}'", owner);
                    var sqlDataReader = tableCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        var tableName = sqlDataReader.GetString(0);
                        tables.Add(new Table { Name = tableName });
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            tables.Sort((x, y) => x.Name.CompareTo(y.Name));
            return tables;
        }
        public List<string> GetSequences(string owner)
        {
            return null;
        }
        public string GetSequences(string tablename, string owner, string column)
        {
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            string tableName = "";
            try
            {
                using (conn)
                {
                    MySqlCommand seqCommand = conn.CreateCommand();
                    seqCommand.CommandText = @"select 
                b.sequence_name
                from
                information_schema.columns a
                inner join information_schema.sequences b on a.column_default like 'nextval(\''||b.sequence_name||'%'
                where
                a.table_schema='" + owner + "' and a.table_name='" + tablename + "' and a.column_name='" + column + "'";
                    MySqlDataReader seqReader = seqCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    while (seqReader.Read())
                    {
                        tableName = seqReader.GetString(0);

                        // sequences.Add(tableName);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return tableName;
        }
        public List<string> GetSequences(List<Table> tables)
        {
            var sequences = new List<string>();
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            try
            {
                using (conn)
                {
                    MySqlCommand seqCommand = conn.CreateCommand();
                    seqCommand.CommandText = "select sequence_name from information_schema.sequences";
                    MySqlDataReader seqReader = seqCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (seqReader.Read())
                    {
                        string tableName = seqReader.GetString(0);

                        sequences.Add(tableName);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return sequences;
        }

        #endregion

        public PrimaryKey DeterminePrimaryKeys(Table table)
        {
            var primaryKeys = table.Columns.Where(x => x.IsPrimaryKey.Equals(true)).ToList();

            if (primaryKeys.Count() == 1)
            {
                var c = primaryKeys.First();
                var key = new PrimaryKey
                {
                    Type = PrimaryKeyType.PrimaryKey,
                    Columns = { c }
                };
                return key;
            }

            if (primaryKeys.Count() > 1)
            {
                // Composite key
                var key = new PrimaryKey
                {
                    Type = PrimaryKeyType.CompositeKey,
                    Columns = primaryKeys
                };

                return key;
            }

            return null;
        }

        public IList<ForeignKey> DetermineForeignKeyReferences(Table table)
        {
            var foreignKeys = (from c in table.Columns
                               where c.IsForeignKey
                               group c by new { c.ConstraintName, c.ForeignKeyTableName } into g
                               select new ForeignKey
                               {
                                   Name = g.Key.ConstraintName,
                                   References = g.Key.ForeignKeyTableName,
                                   Columns = g.ToList(),
                                   UniquePropertyName = g.Key.ForeignKeyTableName
                               }).ToList();

            Table.SetUniqueNamesForForeignKeyProperties(foreignKeys);

            return foreignKeys;
        }

        private string GetForeignKeyReferenceTableName(string selectedTableName, string columnName)
        {
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            object referencedTableName;
            try
            {
                using (conn)
                {
                    MySqlCommand tableCommand = conn.CreateCommand();
                    tableCommand.CommandText = String.Format(
                        @"SELECT ke.referenced_table_name parent, ke.table_name child, ke.constraint_name
                    FROM
                    information_schema.KEY_COLUMN_USAGE ke
                    WHERE
                    ke.referenced_table_name IS NOT NULL and ke.table_name = '{0}' and ke.column_name = '{1}'
                    ORDER BY
                    ke.table_name",
                        selectedTableName, columnName);
                    referencedTableName = tableCommand.ExecuteScalar();
                }
            }
            finally
            {
                conn.Close();
            }
            return (string)referencedTableName;
        }

        // http://blog.sqlauthority.com/2006/11/01/sql-server-query-to-display-foreign-key-relationships-and-name-of-the-constraint-for-each-table-in-database/
        private IList<HasMany> DetermineHasManyRelationships(Table table)
        {
            var hasManyRelationships = new List<HasMany>();
            var conn = new MySqlConnection(connectionStr);
            conn.Open();
            try
            {
                using (conn)
                {
                    using (var command = new MySqlCommand())
                    {
                        command.Connection = conn;
                        command.CommandText =
                            String.Format(
                                @"
                          SELECT
                    ke.referenced_table_name parent,
                    ke.table_name child,
                    ke.constraint_name
                    FROM
                    information_schema.KEY_COLUMN_USAGE ke
                    WHERE
                    ke.referenced_table_name = '{0}' and ke.constraint_schema = '{1}'
                    ORDER BY
                    ke.table_name",
                                table.Name, conn.Database);
                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            hasManyRelationships.Add(new HasMany
                            {
                                Reference = reader.GetString(1)
                            });
                        }

                     
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return hasManyRelationships;
        }
    }
  
}
