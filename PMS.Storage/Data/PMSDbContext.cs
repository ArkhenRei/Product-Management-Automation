using Microsoft.EntityFrameworkCore;
using PMS.API.Models;

namespace PMS.API.Data
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
    }
}
