using Microsoft.AspNetCore.Mvc;
using PMS.API.Models;
using PMS.Service.Services;

namespace PMS.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();
            return Ok(result);
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _productService.AddProduct(product);
            return Ok(result);
        }
        
        [HttpGet("get-product-by-id")] //fix frontend route
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var result = await _productService.GetProduct(id);

            if(result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpPut("update-product")] //fix frontend toute
        public async Task<IActionResult> UpdateProduct(Guid id, Product updateProduct)
        {
            var result = await _productService.UpdateProduct(id, updateProduct);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productService.DeleteProduct(id);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
