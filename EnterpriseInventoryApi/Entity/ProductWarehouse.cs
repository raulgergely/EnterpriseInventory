namespace EnterpriseInventoryApi.Entity
{
    public class ProductWarehouse
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }

        public int StockQuantity { get; set; }
    }
}