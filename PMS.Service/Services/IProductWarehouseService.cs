using PMS.Storage.Models;

namespace PMS.Service.Services
{
    public interface IProductWarehouseService
    {
        Task<List<ProductWarehouse>> GetAllImportExport();  
    }
}
