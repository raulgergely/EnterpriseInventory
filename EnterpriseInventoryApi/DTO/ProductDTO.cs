namespace EnterpriseInventoryApi.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int MinimumStockLevel { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}