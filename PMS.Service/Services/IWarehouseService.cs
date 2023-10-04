using PMS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Service.Services
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse> AddWarehouse(Warehouse warehouse);  
        Task<Warehouse> UpdateWarehouse(Guid id, Warehouse updateWarehouse);
        Task<Warehouse> DeleteWarehouse(Guid id);
    }
}
