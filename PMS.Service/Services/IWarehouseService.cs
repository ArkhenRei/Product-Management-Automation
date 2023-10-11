using PMS.Storage.Models;

namespace PMS.Service.Services
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task<Warehouse> AddWarehouse(Warehouse warehouse);
        Task<Warehouse> UpdateWarehouse(int id, Warehouse updateWarehouse);
        Task DeleteWarehouse(int id);
        ProductWarehouse AddProductToWarehouse(int warehouseId, Guid productId);
        void RemoveProduct(int warehouseId, Guid productId);
    }
}
