using Application.Common.Interfaces;
using Application.DTOs.Auth;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Constants;

namespace Application.Features.Auth.Commands;

public record LoginCommand(LoginRequest Request) : IRequest<BaseResponse<AuthResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, BaseResponse<AuthResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly ICacheService _cacheService;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        ICacheService cacheService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _cacheService = cacheService;
    }

    public async Task<BaseResponse<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Try to get user from cache first
        var cacheKey = CacheKeys.UserByEmail(request.Request.Email);
        var user = await _cacheService.GetAsync<Domain.Entities.User>(cacheKey, cancellationToken);
        
        if (user == null)
        {
            // Get from database if not in cache
            user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Request.Email);
            
            if (user != null)
            {
                // Cache the user for future requests
                await _cacheService.SetAsync(cacheKey, user, CacheSettings.UserCacheExpiry, cancellationToken);
            }
        }
        
        if (user == null || !_passwordHasher.VerifyPassword(request.Request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password");
        }

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

        // Update cache with new refresh token
        await _cacheService.SetAsync(cacheKey, user, CacheSettings.UserCacheExpiry, cancellationToken);
        await _cacheService.SetAsync(CacheKeys.UserById(user.Id), user, CacheSettings.UserCacheExpiry, cancellationToken);

        var response = new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Name, // Using Name field
                LastName = "", // Empty since we only have Name
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            }
        };

        return BaseResponse<AuthResponse>.SuccessResult(response, "Login successful");
    }
}
