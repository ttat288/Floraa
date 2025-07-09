using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Auth.Commands;

public record CompleteAccountCommand(CompleteAccountRequest Request) : IRequest<BaseResponse<AuthResponse>>;

public class CompleteAccountCommandHandler : IRequestHandler<CompleteAccountCommand, BaseResponse<AuthResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public CompleteAccountCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<BaseResponse<AuthResponse>> Handle(CompleteAccountCommand request, CancellationToken cancellationToken)
    {
        // Find activation record
        var activation = await _unitOfWork.AccountActivations
            .FirstOrDefaultAsync(a => a.ActivationToken == request.Request.ActivationToken && !a.IsUsed);

        if (activation == null)
        {
            throw new ValidationException(new[] { "Invalid or expired activation token" });
        }

        if (activation.ExpiresAt < DateTime.UtcNow)
        {
            throw new ValidationException(new[] { "Activation token has expired" });
        }

        // Get user
        var user = await _unitOfWork.Users.GetByIdAsync(activation.UserId);
        if (user == null)
        {
            throw new NotFoundException("User", activation.UserId);
        }

        // Update user information
        user.Name = request.Request.Name;
        user.PhoneNumber = request.Request.PhoneNumber;
        user.Address = request.Request.Address;
        user.PasswordHash = _passwordHasher.HashPassword(request.Request.NewPassword);
        user.IsActive = true;

        _unitOfWork.Users.Update(user);

        // Mark activation as used
        activation.IsUsed = true;
        activation.UsedAt = DateTime.UtcNow;
        _unitOfWork.AccountActivations.Update(activation);

        await _unitOfWork.SaveChangesAsync();

        // Generate tokens
        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Create refresh token record
        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            RefreshTokenCode = Guid.NewGuid().ToString(),
            RefreshTokenValue = refreshToken,
            UserId = user.Id,
            JwtId = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync();

        var response = new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Name,
                LastName = "",
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            }
        };

        return BaseResponse<AuthResponse>.SuccessResult(response, "Account setup completed successfully");
    }
}
