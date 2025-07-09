using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class CompleteAccountRequest
{
    [Required]
    public string ActivationToken { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Address { get; set; }

    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
