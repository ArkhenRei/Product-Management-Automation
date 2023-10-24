namespace PMS.Storage.Models
{
    public class ProductWarehouse : BaseEntity<int>
    {
        public Guid ProductsId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public InOrOutEnum Enum { get; set; }    
    }
}
