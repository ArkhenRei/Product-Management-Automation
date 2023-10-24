using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Storage.Models;
using PMS.Storage.Repository;

namespace PMS.Service.Services
{
    public interface IProductService 
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> AddProduct(Product product);
        Task<Product> GetProduct(Guid id);
        Task<Product> UpdateProduct([FromBody]Guid id, Product updateProductRequest); 
        Task DeleteProduct(Guid id);
    }
}
