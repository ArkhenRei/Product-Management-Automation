using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models
{
    public class WarehouseProduct
    {
        [Key]
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public bool InOrOut { get; set; }   
    }
}
