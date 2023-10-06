using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Storage.Repository;

namespace PMS.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product, Guid> _repository;
        

        public ProductService(IGenericRepository<Product, Guid> genericRepository)
        {
            _repository = genericRepository;           
        }   

        public async Task<Product> AddProduct(Product product)
        {
            //product.Id = Guid.NewGuid();

            product = await _repository.InsertAsync(product);
            await _repository.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _repository.GetAllAsync();
            return products;
            
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            //var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<Product> UpdateProduct([FromBody]Guid id, Product updateProductRequest)
        {
            var product = await _repository.GetByIdAsync(id);

            product.Name = updateProductRequest.Name;
            product.Type = updateProductRequest.Type;
            product.Color = updateProductRequest.Color;
            product.Price = updateProductRequest.Price;

            await _repository.SaveChangesAsync();
            return product;
        }
    }
}
