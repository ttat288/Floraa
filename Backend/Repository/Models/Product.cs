using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Products")]
    public class Product : BaseEntity
    {


        [Required]
        [StringLength(200)]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }

        public int StockQuantity { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(1000)]
        public string? ImageUrls { get; set; } // JSON array of image URLs

        public bool IsActive { get; set; } = true;

        public bool IsFeatured { get; set; } = false;


        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        public Guid CategoryId { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
