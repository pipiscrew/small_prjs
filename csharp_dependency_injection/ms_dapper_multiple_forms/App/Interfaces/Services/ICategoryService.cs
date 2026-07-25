using Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetListAsync(string searchLike = "");
        Task<long> InsertReturnIdAsync(Category obj);
        Task<bool> UpdateAsync(Category obj);
        Task<bool> DeleteAsync(long id);
        Task<Category> GetAsync(long id);
        Task<List<Category>> GetComboListAsync();
        Task<DataTable> GetDatatableAsync();

        //user custom method overridden on CategoryService and virtual on CategoryServiceBase
        Task<IEnumerable<Category>> GetRecords();
    }
}
