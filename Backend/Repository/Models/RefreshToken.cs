using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Token { get; set; } = string.Empty;

        [Required]
        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; } = false;

        public DateTime? RevokedAt { get; set; }

        [StringLength(255)]
        public string? ReplacedByToken { get; set; }

        [StringLength(500)]
        public string? ReasonRevoked { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        // Helper properties
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
