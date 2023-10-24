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
        private readonly IProductWarehouseService _productWarehouseService;

        public WarehouseController(IWarehouseService warehouseService, IProductWarehouseService productWarehouseService)
        {
            _warehouseService = warehouseService;
            _productWarehouseService = productWarehouseService;
        }
        [HttpGet("get-all-warehouses")]
        public async Task<IActionResult> GetAllWarehousesAsync()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> AddWarehouseAsync(Warehouse warehouse)
        {
            var result = await _warehouseService.AddWarehouse(warehouse);
            return Ok(result);
        }

        [HttpPost("update-warehouse")]
        public async Task<IActionResult> UpdateWarehouseAsync(int id, Warehouse updateWarehouse)
        {
            var result = await _warehouseService.UpdateWarehouse(id, updateWarehouse);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("get-warehouse-by-id")]
        public async Task<IActionResult> GetWarehouseByIdAsync(int id)
        {
            var result = await _warehouseService.GetWarehouseByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

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

        [HttpGet("get-all-importexport")]
        public async Task<IActionResult> GetAllImportExport()
        {
            var result = await _productWarehouseService.GetAllImportExport();
            return Ok(result);
        }
    }
}
