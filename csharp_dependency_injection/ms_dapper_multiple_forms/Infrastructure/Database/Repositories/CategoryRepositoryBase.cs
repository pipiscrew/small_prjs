using App.Interfaces.Repositories;
using Dapper;
using Domain;
using Infrastructure.Database.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repositories
{
    public class CategoryRepositoryBase : ICategoryRepository
    {
        protected readonly IConnectionFactory _connectionFactory;

        public CategoryRepositoryBase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Category> GetAsync(long id)
        {
            var sql = @"SELECT * FROM ""categories"" WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var result = await db.QuerySingleOrDefaultAsync<Category>(sql, new { id = id }).ConfigureAwait(false);
                return result;
            }
        }

        public async Task<IEnumerable<Category>> GetListAsync(string searchLike = "")
        {
            var sql = @"SELECT * FROM ""categories""";

            using (var db = _connectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Category>(sql).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(searchLike))
                {
                    result = result.Where(item => item.GetType().GetProperties()
                            .Any(prop => prop.PropertyType == typeof(string) && prop.GetValue(item, null) != null &&
                             prop.GetValue(item, null).ToString().IndexOf(searchLike, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                return result;
            }
        }

        public async Task<List<Category>> GetComboListAsync()
        {
            var sql = @"SELECT * FROM ""categories""";

            using (var db = _connectionFactory.CreateConnection())
            {
                var products = await db.QueryAsync<Category>(sql).ConfigureAwait(false);

                var result = products.ToList();
                result.Insert(0, new Category() { id = 0, title = "" });

                return result;
            }
        }

        public async Task<long> InsertReturnIdAsync(Category obj)
        {
            var sql = @"INSERT INTO ""categories"" (title) VALUES (@title)";

            using (var db = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await db.ExecuteAsync(sql, obj).ConfigureAwait(false);

                if (rowsAffected > 0)
                {
                    var selectCommand = "SELECT last_insert_rowid()";

                    var id = await db.ExecuteScalarAsync<long>(selectCommand).ConfigureAwait(false);
                    return id;
                }

                throw new Exception("Insert failed - no rows affected");
            }
        }

        public async Task<bool> UpdateAsync(Category obj)
        {
            var sql = @"UPDATE ""categories"" SET title = @title WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await db.ExecuteAsync(sql, obj).ConfigureAwait(false);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var sql = @"DELETE FROM ""categories"" WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await db.ExecuteAsync(sql, new { id = id }).ConfigureAwait(false);

                return rowsAffected > 0;
            }
        }

        public async Task<DataTable> GetDatatableAsync()
        {
            var sql = @"SELECT * FROM ""categories""";

            using (var db = _connectionFactory.CreateConnection())
            {
                using (var reader = await db.ExecuteReaderAsync(sql).ConfigureAwait(false))
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }

        //user custom method that exists to CategoryRepository must have virtual method here
        public virtual Task<IEnumerable<Category>> GetRecords()
        {
            throw new NotImplementedException("Override in derived class");
        }
    }
}
