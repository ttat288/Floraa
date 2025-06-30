using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class Occasion : BaseActiveEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<TargetAudience> TargetAudiences { get; set; } = new List<TargetAudience>();
    public virtual ICollection<ProductOccasion> ProductOccasions { get; set; } = new List<ProductOccasion>();
}
