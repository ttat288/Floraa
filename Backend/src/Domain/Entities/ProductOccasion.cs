using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class ProductOccasion : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid OccasionId { get; set; }

    // Navigation properties
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("OccasionId")]
    public virtual Occasion Occasion { get; set; } = null!;
}
