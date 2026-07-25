using App.Interfaces.Repositories;
using Dapper;
using Domain;
using Infrastructure.Database.Common;
using Infrastructure.Database.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repositories
{
    /// <summary>
    /// This file will not be overwritten. You can put additional ProductRepository code in this class 
    /// </summary>
    public class ProductRepository : ProductRepositoryBase, IProductRepository
    {
        public ProductRepository(IConnectionFactory connectionFactory) : base(connectionFactory) { }

        //user custom method
        //must declared as virtual to ProductRepositoryBase
        //must declared to IProductRepository + IProductService + use it on ProductService
        public override async Task<IEnumerable<Product>> GetRecords()
        {
            string query = "SELECT * from products WHERE Id IN @ids";

            using (var db = _connectionFactory.CreateConnection())
            {
                var ids = new int[] { 1, 2, 3 };
                return await db.QueryAsync<Product>(query, new
                {
                    ids = ids
                });
            }
        }
    }
}
