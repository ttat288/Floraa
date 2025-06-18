using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;


        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
