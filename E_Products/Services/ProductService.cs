using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Products.Data;
using E_Products.Model;
using E_Products.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace E_Products.Services
{
    public class ProductService : IProductInterface
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Product Added Successfully";
        }

        public async Task<string> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Product Deleted Successfully";
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return "Product Updated Successfully";
        }
    }
}