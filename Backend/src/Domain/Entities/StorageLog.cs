using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

// StorageLog không cần soft delete - dùng BaseEntity
public class StorageLog : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    public int Quantity { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty; // 'Import', 'Export'

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required]
    public Guid CreatedByUserId { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;

    [ForeignKey("CreatedByUserId")]
    public virtual User CreatedByUser { get; set; } = null!;
}
