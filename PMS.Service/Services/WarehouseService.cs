using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;

namespace PMS.Service.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly PMSDbContext _context;

        public WarehouseService(PMSDbContext context)
        {
            _context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            return warehouses;
        }

        public async Task<Warehouse> AddWarehouse(Warehouse warehouse)
        {
            warehouse.WarehouseId = Guid.NewGuid();

            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Warehouse> UpdateWarehouse(Guid id, Warehouse updateWarehouse)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            warehouse.WarehouseName = updateWarehouse.WarehouseName;
            warehouse.Capacity = updateWarehouse.Capacity;

            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Warehouse> DeleteWarehouse(Guid id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }
    }
}
