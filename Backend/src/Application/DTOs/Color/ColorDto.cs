namespace Application.DTOs.Color;

public class ColorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateColorRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateColorRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
