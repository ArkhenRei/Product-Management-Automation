using Microsoft.EntityFrameworkCore;
using PMS.API.Models;

namespace PMS.API.Data
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<WarehouseModel> Warehouses { get; set; }

        public DbSet<WarehouseProductsModel> WarehouseProducts { get; set;}
    }
}
