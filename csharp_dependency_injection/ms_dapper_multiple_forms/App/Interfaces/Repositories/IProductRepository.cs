using Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Interfaces.Repositories
{
    public interface IProductRepository
    {
         Task<IEnumerable<Product>> GetListAsync(string searchLike = "");
         Task<long> InsertReturnIdAsync(Product obj);
         Task<bool> UpdateAsync(Product obj);
         Task<bool> DeleteAsync(long id);
         Task<Product> GetAsync(long id);
         Task<List<Product>> GetComboListAsync();
         Task<DataTable> GetDatatableAsync();

         //user custom method overridden on ProductRepository and virtual on ProductRepositoryBase
         Task<IEnumerable<Product>> GetRecords();
    }
}
