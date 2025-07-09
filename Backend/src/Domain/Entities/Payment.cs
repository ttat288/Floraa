using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

// Payment không cần soft delete - dùng BaseEntity
public class Payment : BaseEntity
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Method { get; set; } = string.Empty; // 'COD', 'VNPAY', 'MOMO', 'BANK'

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending"; // 'Pending', 'Success', 'Failed'

    [MaxLength(200)]
    public string? TransactionCode { get; set; }

    [MaxLength(200)]
    public string? PayerName { get; set; }

    public DateTime? PaymentTime { get; set; }

    // Navigation properties
    [ForeignKey("OrderId")]
    public virtual Order Order { get; set; } = null!;
}
