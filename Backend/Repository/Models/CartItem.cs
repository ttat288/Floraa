using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("CartItems")]
    public class CartItem : BaseEntity
    {

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        // Navigation properties
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; } = null!;

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
