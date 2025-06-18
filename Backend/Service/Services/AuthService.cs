using AutoMapper;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null || !user.IsActive || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            _unitOfWork.Repository<User>().Update(user);

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(loginDto.RememberMe ? 30 : 7), // 30 days if remember me, 7 days otherwise
                UserId = user.Id
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = refreshTokenEntity.ExpiresAt,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u => u.Email.ToLower() == registerDto.Email.ToLower());

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            // Create new user
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email.ToLower(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                PhoneNumber = registerDto.PhoneNumber,
                Address = registerDto.Address,
                Role = "Customer", // Default role
                IsActive = true,
                EmailConfirmed = true, // Auto confirm for now, in production send confirmation email
                EmailConfirmationToken = null // Will be set if email confirmation is required
            };

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            // Generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = refreshTokenEntity.ExpiresAt,
                User = _mapper.Map<UserDto>(user)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var token = await _unitOfWork.Repository<RefreshToken>()
                .GetSingleOrDefaultAsync(rt => rt.Token == refreshToken);

            if (token == null)
                return false;

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.ReasonRevoked = "Logged out";

            _unitOfWork.Repository<RefreshToken>().Update(token);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.Repository<RefreshToken>()
                .GetSingleWithIncludeAsync(
                    rt => rt.Token == refreshToken,
                    rt => rt.User
                );

            if (token == null || !token.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            // Revoke old token
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.ReasonRevoked = "Replaced by new token";

            // Generate new tokens
            var newAccessToken = _jwtService.GenerateAccessToken(token.User);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Save new refresh token
            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                UserId = token.UserId
            };

            token.ReplacedByToken = newRefreshToken;
            _unitOfWork.Repository<RefreshToken>().Update(token);
            await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshTokenEntity);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = newRefreshTokenEntity.ExpiresAt,
                User = _mapper.Map<UserDto>(token.User)
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
            if (user == null)
                return false;

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u => u.Email.ToLower() == forgotPasswordDto.Email.ToLower());

            if (user == null)
                return false; // Don't reveal if email exists

            user.PasswordResetToken = GeneratePasswordResetToken();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1); // 1 hour expiry

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            // In production, send email with reset link
            // await _emailService.SendPasswordResetEmailAsync(user.Email, user.PasswordResetToken);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u =>
                    u.Email.ToLower() == resetPasswordDto.Email.ToLower() &&
                    u.PasswordResetToken == resetPasswordDto.Token &&
                    u.PasswordResetTokenExpires > DateTime.UtcNow);

            if (user == null)
                return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpires = null;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _unitOfWork.Repository<User>()
                .GetSingleOrDefaultAsync(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.EmailConfirmationToken == token);

            if (user == null)
                return false;

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.Repository<RefreshToken>()
                .GetSingleOrDefaultAsync(rt => rt.Token == refreshToken);

            if (token == null || !token.IsActive)
                return false;

            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            token.ReasonRevoked = "Revoked by user";

            _unitOfWork.Repository<RefreshToken>().Update(token);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RevokeAllTokensAsync(Guid userId)
        {
            var tokens = await _unitOfWork.Repository<RefreshToken>()
                .FindAsync(rt => rt.UserId == userId && rt.IsRevoked == false);

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
                token.ReasonRevoked = "All tokens revoked";
            }

            _unitOfWork.Repository<RefreshToken>().RemoveRange(tokens);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<UserDto?> GetCurrentUserAsync(Guid userId)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        private static string GenerateEmailConfirmationToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private static string GeneratePasswordResetToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
