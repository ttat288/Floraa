using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class TargetAudience : BaseActiveEntity
{
    [Required]
    public Guid OccasionId { get; set; }

    [Required]
    public Guid RecipientTypeId { get; set; }

    // Navigation properties
    [ForeignKey("OccasionId")]
    public virtual Occasion Occasion { get; set; } = null!;

    [ForeignKey("RecipientTypeId")]
    public virtual RecipientType RecipientType { get; set; } = null!;

    public virtual ICollection<ProductTarget> ProductTargets { get; set; } = new List<ProductTarget>();
}
