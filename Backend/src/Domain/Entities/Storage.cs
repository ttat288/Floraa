using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Storage : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    public int CurrentStock { get; set; } = 0;

    // Navigation properties
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;
}
