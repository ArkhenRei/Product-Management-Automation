using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;

namespace PMS.Service.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> AddProduct(Product product);
        Task<Product> GetProduct(Guid id);
        Task<Product> UpdateProduct([FromBody]Guid id, Product updateProductRequest); 
        Task<Product> DeleteProduct(Guid id);
    }
}
