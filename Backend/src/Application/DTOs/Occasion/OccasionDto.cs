namespace Application.DTOs.Occasion;

public class OccasionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateOccasionRequest
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateOccasionRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
