using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

// RefreshToken không cần soft delete - dùng BaseEntity
public class RefreshToken : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string RefreshTokenCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string RefreshTokenValue { get; set; } = string.Empty;

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string JwtId { get; set; } = string.Empty;

    public bool IsUsed { get; set; } = false;

    public bool IsRevoked { get; set; } = false;

    public DateTime ExpiresAt { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
