using App.Interfaces.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    /// <summary>
    /// This file will not be overwritten. You can put additional ProductService code in this class 
    /// </summary>
    public class ProductService : ProductServiceBase
    {
        public ProductService(IProductRepository productRepository) : base(productRepository) { }

        //user custom method
        //must declared as virtual to ProductServiceBase
        //must declared to IProductService
        public override async Task<IEnumerable<Product>> GetRecords()
        {
            //call method from base 
            //var x = GetComboListAsync();

            //use custom function from ProductRepository
            return await _productRepository.GetRecords();
        }


        public override async Task<string> GetProductURL(string id)
        {
            return (await _productRepository.GetAsync(long.Parse(id))).url;
        }
    }
}
