using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
    public class Product
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Foreign Key Relationship with Category
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
