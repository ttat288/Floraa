using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<SubProduct> SubProducts { get; set; } = new List<SubProduct>();
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public virtual ICollection<ShopCategory> ShopCategories { get; set; } = new List<ShopCategory>();
}
