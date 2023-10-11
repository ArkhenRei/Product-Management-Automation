using PMS.Storage.Models;

namespace PMS.API.Models
{
    public class Product : BaseEntity<Guid>
    {
        public override Guid Id { get => base.Id; set => base.Id = Guid.NewGuid(); }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public ICollection<Warehouse> Warehouse { get; } = new List<Warehouse>();
    }
}   
