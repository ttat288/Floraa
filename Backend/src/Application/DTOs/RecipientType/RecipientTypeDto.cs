namespace Application.DTOs.RecipientType;

public class RecipientTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateRecipientTypeRequest
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateRecipientTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
