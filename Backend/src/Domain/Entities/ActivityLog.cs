using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class ActivityLog : BaseEntity
{
    public Guid? UserId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Action { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? IPAddress { get; set; }

    [Required]
    [MaxLength(50)]
    public string ActionType { get; set; } = string.Empty; // 'Login', 'Order', 'Update', 'Delete', 'Create', 'Voucher', 'Other'

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
