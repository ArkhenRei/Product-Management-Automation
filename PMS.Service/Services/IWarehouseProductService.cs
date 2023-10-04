using PMS.API.Models;

namespace PMS.Service.Services
{
    public interface IWarehouseProductService
    {
        Task<WarehouseProduct> AddProductToWarehouse(Guid productId, Guid warehouseId, WarehouseProduct warehouseProduct);
    }
}
