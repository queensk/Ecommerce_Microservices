using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Products.Model;

namespace E_Products.Services.IService
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(Guid id);

        Task<string> AddProductAsync(Product product);

        Task<string> DeleteProductAsync(Product product);

        Task<string> UpdateProductAsync(Product product);
    }
}