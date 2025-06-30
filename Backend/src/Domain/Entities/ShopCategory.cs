using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class ShopCategory : BaseEntity
{
    [Required]
    public Guid ShopId { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    // Navigation properties
    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = null!;
}
