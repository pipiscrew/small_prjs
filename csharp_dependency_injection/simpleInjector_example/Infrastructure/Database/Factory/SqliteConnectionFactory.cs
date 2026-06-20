using System;
using System.Data;
using System.Data.SQLite;

namespace posokanei.Infrastructure.Database.Factory
{
    class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        private readonly string _connectionString;
        
        public SqliteConnectionFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }
    }
}
