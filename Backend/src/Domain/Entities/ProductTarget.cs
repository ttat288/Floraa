using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class ProductTarget : BaseEntity
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid TargetAudienceId { get; set; }

    // Navigation properties
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("TargetAudienceId")]
    public virtual TargetAudience TargetAudience { get; set; } = null!;
}
