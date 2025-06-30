using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class SubProduct : BaseEntity
{
    [Required]
    public Guid CategoryId { get; set; }

    [MaxLength(500)]
    public string? Meaning { get; set; }

    // Navigation properties
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
