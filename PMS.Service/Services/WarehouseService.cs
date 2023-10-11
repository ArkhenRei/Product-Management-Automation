using PMS.API.Data;
using PMS.Storage.Models;
using PMS.Storage.Repository;

namespace PMS.Service.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IGenericRepository<Warehouse, int> _genericRepository;
        private readonly PMSDbContext _pmsDbContext;
        public WarehouseService(IGenericRepository<Warehouse, int> genericRepository, PMSDbContext pmsDbContext)    
        {
            _genericRepository = genericRepository;
            _pmsDbContext = pmsDbContext;
        }

        public async Task<Warehouse> AddWarehouse(Warehouse warehouse)
        {
            warehouse = await _genericRepository.InsertAsync(warehouse);
            await _genericRepository.SaveChangesAsync();
            return warehouse;
        }

        public async Task DeleteWarehouse(int id)
        {
            await _genericRepository.DeleteAsync(id);
            await _genericRepository.SaveChangesAsync();    
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            var warehouses = await _genericRepository.GetAllAsync();
            return warehouses;
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await _genericRepository.GetByIdAsync(id);
            return warehouse;
        }

        public async Task<Warehouse> UpdateWarehouse(int id, Warehouse updateWarehouse)
        {
            var warehouse = await _genericRepository.GetByIdAsync(id);
            
            warehouse.Name = updateWarehouse.Name;
            warehouse.Capacity = updateWarehouse.Capacity;

            await _genericRepository.SaveChangesAsync();
            return warehouse;
        }
        //Add product to warehouse
        public ProductWarehouse AddProductToWarehouse(int warehouseId, Guid productId)
        {
            var warehouseProduct = new ProductWarehouse() { WarehouseId = warehouseId, ProductsId = productId };
            _pmsDbContext.ProductWarehouses.AddAsync(warehouseProduct);
            _pmsDbContext.SaveChangesAsync();
            return warehouseProduct;
        }
        //Remove a product from warehouse
        public void RemoveProduct(int warehouseId, Guid productId)
        {
            var warehouseProductToRemove = _pmsDbContext.ProductWarehouses.Where(wp => wp.WarehouseId == warehouseId && wp.ProductsId == productId).FirstOrDefault();
            if (warehouseProductToRemove != null)
            {
                _pmsDbContext.ProductWarehouses.Remove(warehouseProductToRemove);
                _pmsDbContext.SaveChangesAsync();
            }
        }
    }
}
