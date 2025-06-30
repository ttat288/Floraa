using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class Color : BaseActiveEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
