namespace Application.DTOs.Theme;

public class ThemeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateThemeRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateThemeRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
