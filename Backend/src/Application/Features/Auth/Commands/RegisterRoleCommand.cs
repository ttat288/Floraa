using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using MediatR;
using System.Security.Cryptography;

namespace Application.Features.Auth.Commands;

public record RegisterRoleCommand(RegisterRoleAccount Request) : IRequest<BaseResponse<string>>;

public class RegisterRoleCommandHandler : IRequestHandler<RegisterRoleCommand, BaseResponse<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailService _emailService;

    public RegisterRoleCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
    }

    public async Task<BaseResponse<string>> Handle(RegisterRoleCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Request.Email);
        if (existingUser != null)
        {
            throw new ValidationException(new[] { "Email already exists" });
        }

        // Generate temporary password and activation token
        var tempPassword = GenerateTemporaryPassword();
        var activationToken = GenerateActivationToken();

        var user = new User
        {
            Email = request.Request.Email,
            PasswordHash = _passwordHasher.HashPassword(tempPassword),
            Name = string.Empty, // Will be filled when user completes profile
            PhoneNumber = string.Empty,
            Role = request.Request.Role,
            IsActive = false // Account is inactive until user completes setup
        };

        await _unitOfWork.Users.AddAsync(user);

        // Create account activation record
        var activation = new AccountActivation
        {
            UserId = user.Id,
            ActivationToken = activationToken,
            TempPassword = tempPassword,
            ExpiresAt = DateTime.UtcNow.AddDays(7), // Token expires in 7 days
            IsUsed = false
        };

        await _unitOfWork.AccountActivations.AddAsync(activation);
        await _unitOfWork.SaveChangesAsync();

        // Send invitation email
        await _emailService.SendAccountInvitationAsync(
            user.Email,
            request.Request.Role.ToString(),
            activationToken,
            tempPassword);

        return BaseResponse<string>.SuccessResult(
            "Account invitation sent successfully",
            "Registration successful");
    }

    private string GenerateTemporaryPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private string GenerateActivationToken()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[32];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
