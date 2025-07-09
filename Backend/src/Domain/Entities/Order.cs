using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Order : BaseSoftDeleteEntity
{
    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = string.Empty;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    [Required]
    [MaxLength(200)]
    public string ReceiverName { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? ReceiverPhone { get; set; }

    [MaxLength(500)]
    public string? ShippingAddress { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    [MaxLength(1000)]
    public string? Note { get; set; }

    public DateTime? ShippedAt { get; set; }

    [MaxLength(500)]
    public string? BeforeDeliveryImage { get; set; }

    [MaxLength(500)]
    public string? AfterDeliveryImage { get; set; }

    public int? Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTime? CommentAt { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
