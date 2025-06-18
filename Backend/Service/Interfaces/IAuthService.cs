using Service.DTOs;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> LogoutAsync(string refreshToken);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<bool> ConfirmEmailAsync(string email, string token);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> RevokeAllTokensAsync(Guid userId);
        Task<UserDto?> GetCurrentUserAsync(Guid userId);
    }
}
