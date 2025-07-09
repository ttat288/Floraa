using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

// Base entity cho dữ liệu tạm thời - không cần soft delete
public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

// Base entity cho dữ liệu cần soft delete
public abstract class BaseSoftDeleteEntity : BaseEntity
{
    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }
}

public abstract class BaseActiveEntity : BaseSoftDeleteEntity
{
    public bool IsActive { get; set; } = true;
}

public abstract class BaseAuditEntity : BaseActiveEntity
{
    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}
