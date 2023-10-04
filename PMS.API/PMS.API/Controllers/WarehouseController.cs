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
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService as WarehouseService;
        }

        [HttpGet("warehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehouses();
            return Ok(warehouses);
        }

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> AddWarehouse(Warehouse warehouse)
        {
            var result = await _warehouseService.AddWarehouse(warehouse);
            return Ok(result);
        }

        [HttpPut("edit-warehouse")]
        public async Task<IActionResult> UpdateWarehouse(Guid id, Warehouse updateWarehouse)
        {
            var result = await _warehouseService.UpdateWarehouse(id, updateWarehouse);

            if (result == null)
                return BadRequest();
        
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouse(Guid id)
        {
            var result = await _warehouseService.DeleteWarehouse(id);

            if (result == null)
                return BadRequest();
            
            return Ok(result);
        }
    }
}
