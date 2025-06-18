using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
       
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Processing, Shipped, Delivered, Cancelled

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [StringLength(15)]
        public string? CustomerPhone { get; set; }

        [Required]
        [StringLength(500)]
        public string ShippingAddress { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Notes { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }

        // Foreign Keys
        public Guid? UserId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
