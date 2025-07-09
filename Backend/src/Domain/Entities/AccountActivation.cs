using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class AccountActivation : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(500)]
    public string ActivationToken { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string TempPassword { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; } = false;

    public DateTime? UsedAt { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
