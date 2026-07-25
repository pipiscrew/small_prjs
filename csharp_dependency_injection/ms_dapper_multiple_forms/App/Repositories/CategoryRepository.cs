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
    /// This file will not be overwritten. You can put additional CategoryRepository code in this class 
    /// </summary>
    public class CategoryRepository : CategoryRepositoryBase, ICategoryRepository
    {
        public CategoryRepository(IConnectionFactory connectionFactory) : base(connectionFactory) { }

        //user custom method
        //must declared as virtual to CategoryRepositoryBase
        //must declared to ICategoryRepository + ICategoryService + use it on CategoryService
        public override async Task<IEnumerable<Category>> GetRecords()
        {
            string query = "SELECT * from categories WHERE Id IN @ids";

            using (var db = _connectionFactory.CreateConnection())
            {
                var ids = new int[] { 1, 2, 3 };
                return await db.QueryAsync<Category>(query, new
                {
                    ids = ids
                });
            }
        }
    }
}
