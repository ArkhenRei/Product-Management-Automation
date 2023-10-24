using Microsoft.EntityFrameworkCore;
using PMS.API.Models;
using PMS.Storage.Models;

namespace PMS.API.Data
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        
        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Warehouse)
                .WithMany(e => e.Products)
                .UsingEntity<ProductWarehouse>();
        }
    }

}
