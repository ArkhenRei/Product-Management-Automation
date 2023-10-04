using PMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> AddProduct(Product product);
        Task<Product> GetProduct(Guid id);
        Task<Product> UpdateProduct(Guid id, Product updateProduct);
        Task<Product> DeleteProduct(Guid id);
    }
}
