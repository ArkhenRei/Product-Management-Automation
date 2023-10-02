using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly PMSDbContext _pmsDbContext;

        public WarehouseController(PMSDbContext pmsDbContext)
        {
            _pmsDbContext = pmsDbContext;
        }

        [HttpGet("warehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _pmsDbContext.Warehouses.ToListAsync();
            return Ok(warehouses);
        }

        [HttpPost("add-warehouse")]
        public async Task<IActionResult> AddWarehouse([FromBody] WarehouseModel warehouseModel)
        {
            await _pmsDbContext.Warehouses.AddAsync(warehouseModel);
            await _pmsDbContext.SaveChangesAsync();
            return Ok(warehouseModel);
        }

        [HttpPut("edit-warehouse")]
        public async Task<IActionResult> UpdateWarehouse(int id, WarehouseModel updateWarehouse)    
        {
            var warehouse = await _pmsDbContext.Warehouses.FindAsync(id);

            if (warehouse == null)
                return BadRequest();

            warehouse.WarehouseName = updateWarehouse.WarehouseName;

            await _pmsDbContext.SaveChangesAsync();
            return Ok(warehouse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var warehouse = await _pmsDbContext.Warehouses.FindAsync(id);
            
            if(warehouse == null)
                return BadRequest();

            _pmsDbContext.Warehouses.Remove(warehouse);
            await _pmsDbContext.SaveChangesAsync();
            return Ok(warehouse);
        }

        [HttpPost("add-product-towarehouse")]


    }
}
