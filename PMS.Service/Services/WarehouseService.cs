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
            warehouse.FilledCapacity = 0;
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
        public async Task<ProductWarehouse> AddProductToWarehouse(int warehouseId, Guid productId, int quantity)
        {
            var product = _pmsDbContext.Products.Find(productId);
            // Get the warehouse from the database.
            var warehouse = _pmsDbContext.Warehouse.Find(warehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException("Warehouse not found.");
            }

            // Check if the warehouse has enough capacity.
            if (quantity > warehouse.Capacity - warehouse.FilledCapacity)
            {
                throw new ArgumentException("Not enough capacity in warehouse.");
            }

            // Create a new ProductWarehouse object.
            var warehouseProduct = new ProductWarehouse
            {
                WarehouseId = warehouseId,
                ProductsId = productId,
                Quantity = quantity
            };

            // Add the new ProductWarehouse object to the database.
            await _pmsDbContext.ProductWarehouses.AddAsync(warehouseProduct);
            await _pmsDbContext.SaveChangesAsync();

            // Update the FilledCapacity of the warehouse.
            warehouse.FilledCapacity += quantity;
            _pmsDbContext.SaveChanges();

            //Update the Stock of the product
            product.Stock += quantity;
            _pmsDbContext.SaveChanges();

            // Return the ProductWarehouse object.
            return warehouseProduct;

            //var warehouseProduct = new ProductWarehouse() { WarehouseId = warehouseId, ProductsId = productId };
            //_pmsDbContext.ProductWarehouses.AddAsync(warehouseProduct);
            //_pmsDbContext.SaveChangesAsync();
            //return warehouseProduct;
        }
        //Remove a product from warehouse
        public void RemoveProduct(int warehouseId, Guid productId, int quantity)
        {
            var warehouseProductToRemove = _pmsDbContext.ProductWarehouses.Where(wp => wp.WarehouseId == warehouseId && wp.ProductsId == productId).ToList();
            if (warehouseProductToRemove == null)
            {
                throw new ArgumentException("Warehouse product not found.");
            }
            var warehouse = _pmsDbContext.Warehouse.Find(warehouseId);

            // Calculate the sum of the quantities of the warehouse products.
            var sumOfQuantities = warehouseProductToRemove.Sum(wp => wp.Quantity);

            // If the sum of the quantities is greater than or equal to the quantity to remove, remove the quantity to remove from each warehouse product.
            if (sumOfQuantities >= quantity)
            {
                warehouse.FilledCapacity -= quantity;
                _pmsDbContext.SaveChanges();
                // Sort the warehouse products by quantity in descending order.
                warehouseProductToRemove.Sort((x, y) => y.Quantity.CompareTo(x.Quantity));
                foreach (var warehouseProduct in warehouseProductToRemove)
                {
                    if (warehouseProduct.Quantity >= quantity)
                    {
                        warehouseProduct.Quantity -= quantity;
                        quantity = 0;

                        if(warehouseProduct.Quantity <= 0)
                        {
                            // Delete the warehouse product if its quantity is now zero.
                            _pmsDbContext.ProductWarehouses.Remove(warehouseProduct);
                            _pmsDbContext.SaveChanges();
                        }
                        warehouseProduct.Enum = InOrOutEnum.Out;
                        _pmsDbContext.SaveChanges();
                    }
                    else
                    {
                        quantity -= warehouseProduct.Quantity;
                        warehouseProduct.Quantity = 0;
                        warehouseProduct.Enum = InOrOutEnum.Out;
                        _pmsDbContext.SaveChanges();

                        // Delete the warehouse product if its quantity is now zero.
                        _pmsDbContext.ProductWarehouses.Remove(warehouseProduct);
                        _pmsDbContext.SaveChanges();
                    }

                    // If all of the quantity has been removed, break out of the loop.
                    if (quantity == 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Not enough products in warehouse.");
            }

            // Save the changes to the database.
            _pmsDbContext.SaveChanges();

            //if (warehouseProductToRemove != null)
            //{
            //    _pmsDbContext.ProductWarehouses.Remove(warehouseProductToRemove);
            //    _pmsDbContext.SaveChangesAsync();
            //}
        }
    }
}
