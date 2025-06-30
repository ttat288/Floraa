using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Discount : BaseActiveEntity
{
    public Guid? ProductId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5,2)")]
    public decimal Percent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    // Navigation properties
    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;
}
