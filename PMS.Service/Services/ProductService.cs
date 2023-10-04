using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly PMSDbContext _context;

        public ProductService(PMSDbContext context)
        {
            _context = context;
        }   

        public async Task<Product> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid();

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            
            _context.Products.Remove(product);           
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
            
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<Product> UpdateProduct(Guid id, Product updateProduct)
        {
            var product = await _context.Products.FindAsync(id);

            product.Name = updateProduct.Name;
            product.Type = updateProduct.Type;
            product.Color = updateProduct.Color;
            product.Price = updateProduct.Price;

            await _context.SaveChangesAsync();
            return product;
        }
    }
}
