using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;
using PMS.Service.Services;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductController : ControllerBase
    {
        private readonly IWarehouseProductService _warehouseProductService;

        public WarehouseProductController(
            IWarehouseProductService warehouseProductService)
        {
            _warehouseProductService = warehouseProductService;
        }

        //Warehouse'a ürün eklenecek
        [HttpPost("add-product-warehouse")]
        public async Task<IActionResult> AddProductToWarehouse(Guid productId, Guid warehouseId, WarehouseProduct warehouseProduct)
        {
            try
            {
                 var result = await _warehouseProductService.AddProductToWarehouse(productId, warehouseId, warehouseProduct);
                //return a success message to the user.
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "The product has been successfully added to the warehouse.",
                    Data = result
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
       

    }
}
