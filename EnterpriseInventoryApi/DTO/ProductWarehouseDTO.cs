namespace EnterpriseInventoryApi.DTO
{
    public class ProductWarehouseDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        public int WarehouseId { get; set; }
        public string? WarehouseName { get; set; }

        public int StockQuantity { get; set; }
    }
}