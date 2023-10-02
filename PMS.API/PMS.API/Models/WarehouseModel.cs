using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models
{
    public class WarehouseModel
    {
        [Key]
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int StoreId { get; set; }
    }
}
