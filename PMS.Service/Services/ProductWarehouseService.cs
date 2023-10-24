using PMS.Storage.Models;
using PMS.Storage.Repository;

namespace PMS.Service.Services
{
    public class ProductWarehouseService : IProductWarehouseService
    {
        private readonly IGenericRepository<ProductWarehouse, int> _productWarehouseRepository;

        public ProductWarehouseService(IGenericRepository<ProductWarehouse, int> productWarehouseRepository)
        {
            _productWarehouseRepository = productWarehouseRepository;
        }

        public async Task<List<ProductWarehouse>> GetAllImportExport()
        {
            var result = await _productWarehouseRepository.GetAllAsync();
            return result;
        } 
    }
}
