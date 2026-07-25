using App.Interfaces.Repositories;
using App.Interfaces.Services;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Services
{
    public class ProductServiceBase : IProductService
    {
        protected readonly IProductRepository _productRepository;

        public ProductServiceBase(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetListAsync(string searchLike = "")
        {
            return await _productRepository.GetListAsync(searchLike);
        }

        public async Task<long> InsertReturnIdAsync(Product obj)
        {
            return await _productRepository.InsertReturnIdAsync(obj);
        }

        public async Task<bool> UpdateAsync(Product obj)
        {
            return await _productRepository.UpdateAsync(obj);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<Product> GetAsync(long id)
        {
            return await _productRepository.GetAsync(id);
        }

        public async Task<List<Product>> GetComboListAsync()
        {
            return await _productRepository.GetComboListAsync();
        }

        public async Task<DataTable> GetDatatableAsync()
        {
            return await _productRepository.GetDatatableAsync();
        }

        //user custom method exists to ProductService must have virtual method here
        public virtual Task<IEnumerable<Product>> GetRecords()
        {
            throw new NotImplementedException("Override in derived class");
        }

        public virtual Task<string> GetProductURL(string id)
        {
            throw new NotImplementedException("Override in derived class");
        }
    }
}
