using App.Interfaces.Repositories;
using App.Interfaces.Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Services
{
    public class CategoryServiceBase : ICategoryService
    {
        protected readonly ICategoryRepository _categoryRepository;

        public CategoryServiceBase(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetListAsync(string searchLike = "")
        {
            return await _categoryRepository.GetListAsync(searchLike);
        }

        public async Task<long> InsertReturnIdAsync(Category obj)
        {
            return await _categoryRepository.InsertReturnIdAsync(obj);
        }

        public async Task<bool> UpdateAsync(Category obj)
        {
            return await _categoryRepository.UpdateAsync(obj);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }

        public async Task<Category> GetAsync(long id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        public async Task<List<Category>> GetComboListAsync()
        {
            return await _categoryRepository.GetComboListAsync();
        }

        public async Task<DataTable> GetDatatableAsync()
        {
            return await _categoryRepository.GetDatatableAsync();
        }

        //user custom method exists to CategoryService must have virtual method here
        public virtual Task<IEnumerable<Category>> GetRecords()
        {
            throw new NotImplementedException("Override in derived class");
        }
    }
}
