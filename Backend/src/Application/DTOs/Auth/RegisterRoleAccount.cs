using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class RegisterRoleAccount
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public EmployeeRole Role { get; set; }
}
