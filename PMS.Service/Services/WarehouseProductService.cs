using Microsoft.EntityFrameworkCore;
using PMS.API.Data;
using PMS.API.Models;

namespace PMS.Service.Services
{    
    public class WarehouseProductService : IWarehouseProductService
    {
        private readonly PMSDbContext _context;

        public WarehouseProductService(PMSDbContext context)
        {
            _context = context;
        }

        public async Task<WarehouseProduct> AddProductToWarehouse(Guid productId, Guid warehouseId, WarehouseProduct warehouseProduct)
        {
            try
            {
                //Get the warehouse
                Warehouse warehouse = await _context.Warehouses.FindAsync(warehouseId);
                //Get the product
                Product product = await _context.Products.FindAsync(productId);

                //Check the capacity of the warehouse            
                if (await IsWarehouseFullAsync(warehouseId))
                {
                    throw new Exception("The warehouse does not have enough capacity to add the product.");
                }

                //Add the new product to the warehouse
                warehouseProduct.ProductId = productId;
                warehouseProduct.WarehouseId = warehouseId;
                warehouseProduct.InOrOut = true;
                var result = _context.WarehouseProducts.Add(warehouseProduct);

                //save changes to the database
                await _context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<bool> IsWarehouseFullAsync(Guid warehouseId)
        {
            // Get the total quantity of all products in the warehouse.
            int totalQuantity = await _context.WarehouseProducts
                .Where(w => w.WarehouseId == warehouseId)
                .SumAsync(w => w.Quantity);

            // Get the warehouse capacity.
            Warehouse warehouse = await _context.Warehouses.FindAsync(warehouseId);
            int capacity = warehouse.Capacity;

            // Compare the total quantity to the warehouse capacity.
            return totalQuantity >= capacity;
        }
    }
}