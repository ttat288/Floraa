using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Employee : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ShopId { get; set; }

    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [ForeignKey("ShopId")]
    public virtual Shop Shop { get; set; } = null!;
}
