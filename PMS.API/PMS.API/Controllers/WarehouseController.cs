using Microsoft.AspNetCore.Mvc;
using PMS.Service.Services;
using PMS.Storage.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> AddWarehouseAsync(Warehouse warehouse)
        {
            var result = await _warehouseService.AddWarehouse(warehouse);
            return Ok(result);
        }
        [HttpDelete]
        public async Task DeleteWarehouse(int id)
        {
            await _warehouseService.DeleteWarehouse(id);
        }
        [HttpPost("add-product/{warehouseId}/{productId}")]
        public async Task<IActionResult> AddProductToWarehouse(int warehouseId, Guid productId, int quantity)
        {
            var warehouse = await _warehouseService.AddProductToWarehouse(warehouseId, productId, quantity);

            return Ok(warehouse);
        }

        [HttpPost("remove-product/{warehouseId}/{productId}")]
        public void RemoveProduct(int warehouseId, Guid productId, int quantity)
        {
            _warehouseService.RemoveProduct(warehouseId, productId, quantity);
        }
    }
}
