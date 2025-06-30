using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class RecipientType : BaseActiveEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty; // e.g., "Người yêu", "Gia đình", "Bạn bè"

    // Navigation properties
    public virtual ICollection<TargetAudience> TargetAudiences { get; set; } = new List<TargetAudience>();
}
