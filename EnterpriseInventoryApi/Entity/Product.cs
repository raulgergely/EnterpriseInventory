using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnterpriseInventoryApi.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int MinimumStockLevel { get; set; } = 5;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}