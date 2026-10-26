using System.ComponentModel.DataAnnotations;

namespace EnterpriseInventoryApi.Entity
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}