using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models
{
    public class WarehouseProductsModel
    {
        [Key]
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int StoreId { get; set; }    
    }
}
