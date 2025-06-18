using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "Customer"; // Admin, Staff, Customer

        public bool IsActive { get; set; } = true;

        public bool EmailConfirmed { get; set; } = false;

        [StringLength(255)]
        public string? EmailConfirmationToken { get; set; }

        [StringLength(255)]
        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetTokenExpires { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
