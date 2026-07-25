using App.Interfaces.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    /// <summary>
    /// This file will not be overwritten. You can put additional CategoryService code in this class 
    /// </summary>
    public class CategoryService : CategoryServiceBase
    {
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository) { }

        //user custom method
        //must declared as virtual to CategoryServiceBase
        //must declared to ICategoryService
        public override async Task<IEnumerable<Category>> GetRecords()
        {
            //call method from base 
            //var x = GetComboListAsync();

            //use custom function from CategoryRepository
            return await _categoryRepository.GetRecords();
        }
    }
}
