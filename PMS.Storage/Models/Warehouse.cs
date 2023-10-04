using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models
{
    public class Warehouse
    {
        [Key]
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int Capacity { get; set; }   
    }
}
