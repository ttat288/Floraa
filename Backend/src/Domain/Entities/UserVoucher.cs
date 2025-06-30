using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class UserVoucher : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid VoucherId { get; set; }

    public DateTime? UsedAt { get; set; }

    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    public bool IsUsed { get; set; } = false;

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("VoucherId")]
    public virtual Voucher Voucher { get; set; } = null!;
}
