using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.Service.Services;
using PMS.Storage.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly PMSDbContext _pmsDbContext;

        public WarehouseController(IWarehouseService warehouseService, PMSDbContext pMSDbContext)
        {
            _warehouseService = warehouseService;
            _pmsDbContext = pMSDbContext;
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
        public IActionResult AddProductToWarehouse(int warehouseId, Guid productId)
        {
            var warehouse = _warehouseService.AddProductToWarehouse(warehouseId, productId);

            return Ok(warehouse);
        }

        [HttpPost("remove-product/{warehouseId}/{productId}")]
        public void RemoveProduct(int warehouseId, Guid productId)
        {
            _warehouseService.RemoveProduct(warehouseId, productId);
        }
    }
}
