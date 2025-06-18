using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Carts")]
    public class Cart : BaseEntity
    {

        [StringLength(100)]
        public string? SessionId { get; set; } // For guest users


        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys
        public Guid? UserId { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
