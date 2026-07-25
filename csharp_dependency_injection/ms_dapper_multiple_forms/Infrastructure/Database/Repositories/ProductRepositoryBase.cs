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
    public class ProductRepositoryBase : IProductRepository
    {
        protected readonly IConnectionFactory _connectionFactory;

        public ProductRepositoryBase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product> GetAsync(long id)
        {
            var sql = @"SELECT * FROM ""products"" WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var result = await db.QuerySingleOrDefaultAsync<Product>(sql, new { id = id }).ConfigureAwait(false);
                return result;
            }
        }

        public async Task<IEnumerable<Product>> GetListAsync(string searchLike = "")
        {
            var sql = @"SELECT * FROM ""products""";

            using (var db = _connectionFactory.CreateConnection())
            {
                var result = await db.QueryAsync<Product>(sql).ConfigureAwait(false);

                if (!string.IsNullOrEmpty(searchLike))
                {
                    result = result.Where(item => item.GetType().GetProperties()
                            .Any(prop => prop.PropertyType == typeof(string) && prop.GetValue(item, null) != null &&
                             prop.GetValue(item, null).ToString().IndexOf(searchLike, StringComparison.OrdinalIgnoreCase) >= 0));
                }

                return result;
            }
        }

        public async Task<List<Product>> GetComboListAsync()
        {
            var sql = @"SELECT * FROM ""products""";

            using (var db = _connectionFactory.CreateConnection())
            {
                var products = await db.QueryAsync<Product>(sql).ConfigureAwait(false);

                var result = products.ToList();
                result.Insert(0, new Product() { id = 0, title = "" });

                return result;
            }
        }

        public async Task<long> InsertReturnIdAsync(Product obj)
        {
            var sql = @"INSERT INTO ""products"" (title, url, when2check, dateupdated, smarketab, smarketsklav, smarketbazaar, smarketmymarket, comment, homepage, nutritiontable, category_id, ingredients) VALUES (@title, @url, @when2check, @dateupdated, @smarketab, @smarketsklav, @smarketbazaar, @smarketmymarket, @comment, @homepage, @nutritiontable, @category_id, @ingredients)";

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

        public async Task<bool> UpdateAsync(Product obj)
        {
            var sql = @"UPDATE ""products"" SET title = @title, url = @url, when2check = @when2check, dateupdated = @dateupdated, smarketab = @smarketab, smarketsklav = @smarketsklav, smarketbazaar = @smarketbazaar, smarketmymarket = @smarketmymarket, comment = @comment, homepage = @homepage, nutritiontable = @nutritiontable, category_id = @category_id, ingredients = @ingredients WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await db.ExecuteAsync(sql, obj).ConfigureAwait(false);

                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var sql = @"DELETE FROM ""products"" WHERE id = @id";

            using (var db = _connectionFactory.CreateConnection())
            {
                var rowsAffected = await db.ExecuteAsync(sql, new { id = id }).ConfigureAwait(false);

                return rowsAffected > 0;
            }
        }

        public async Task<DataTable> GetDatatableAsync()
        {
            var sql = @"SELECT * FROM ""products""";

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

        //user custom method that exists to ProductRepository must have virtual method here
        public virtual Task<IEnumerable<Product>> GetRecords()
        {
            throw new NotImplementedException("Override in derived class");
        }
    }
}
