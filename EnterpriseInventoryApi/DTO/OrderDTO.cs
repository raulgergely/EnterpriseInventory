namespace EnterpriseInventoryApi.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }

        public int CustomerId { get; set; }
        public string? CustomerCompanyName { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceAtOrder { get; set; }
    }
}