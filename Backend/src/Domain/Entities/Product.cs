using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Product : BaseActiveEntity
{
    [Required]
    public Guid ShopId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public Guid? ThemeId { get; set; }

    public Guid? OccasionId { get; set; }

    public Guid? ColorId { get; set; }

    public Guid? SubProductId { get; set; }

    [MaxLength(500)]
    public string? Composition { get; set; }

    public int Quantity { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int SoldCount { get; set; } = 0;

    // Navigation properties
    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;

    [ForeignKey("ThemeId")]
    public virtual Theme? Theme { get; set; }

    [ForeignKey("OccasionId")]
    public virtual Occasion? Occasion { get; set; }

    [ForeignKey("ColorId")]
    public virtual Color? Color { get; set; }

    [ForeignKey("SubProductId")]
    public virtual SubProduct? SubProduct { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    public virtual ICollection<Cart> CartItems { get; set; } = new List<Cart>();
    public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();
    public virtual ICollection<StorageLog> StorageLogs { get; set; } = new List<StorageLog>();
    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public virtual ICollection<ProductTarget> ProductTargets { get; set; } = new List<ProductTarget>();
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public virtual ICollection<ProductOccasion> ProductOccasions { get; set; } = new List<ProductOccasion>();
}
