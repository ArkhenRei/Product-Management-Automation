using PMS.API.Models;

namespace PMS.Storage.Models
{
    public class Warehouse : BaseEntity<int>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public int FilledCapacity { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
