using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interfaces;
using API.Extensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// User login
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);

                // Set refresh token in HTTP-only cookie
                SetRefreshTokenCookie(result.RefreshToken, result.ExpiresAt);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// User registration
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            try
            {
                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    return BadRequest(new { message = "Passwords do not match." });
                }

                var result = await _authService.RegisterAsync(registerDto);

                // Set refresh token in HTTP-only cookie
                SetRefreshTokenCookie(result.RefreshToken, result.ExpiresAt);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// User logout
        /// </summary>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    await _authService.LogoutAsync(refreshToken);
                }

                // Clear refresh token cookie
                Response.Cookies.Delete("refreshToken");

                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Refresh access token
        /// </summary>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized(new { message = "Refresh token not found." });
                }

                var result = await _authService.RefreshTokenAsync(refreshToken);

                // Set new refresh token in cookie
                SetRefreshTokenCookie(result.RefreshToken, result.ExpiresAt);

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Change password - Tự động lấy userId từ token
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
                {
                    return BadRequest(new { message = "New passwords do not match." });
                }

                // Tự động lấy userId từ token
                var userId = User.GetUserId();
                var result = await _authService.ChangePasswordAsync(userId, changePasswordDto);

                if (!result)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(new { message = "Password changed successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                await _authService.ForgotPasswordAsync(forgotPasswordDto);
                return Ok(new { message = "If the email exists, a password reset link has been sent." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Reset password
        /// </summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmNewPassword)
                {
                    return BadRequest(new { message = "Passwords do not match." });
                }

                var result = await _authService.ResetPasswordAsync(resetPasswordDto);

                if (!result)
                {
                    return BadRequest(new { message = "Invalid or expired reset token." });
                }

                return Ok(new { message = "Password reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Confirm email
        /// </summary>
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            try
            {
                var result = await _authService.ConfirmEmailAsync(email, token);

                if (!result)
                {
                    return BadRequest(new { message = "Invalid confirmation token." });
                }

                return Ok(new { message = "Email confirmed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get current user info - Tự động lấy từ token
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            try
            {
                // Tự động lấy userId từ token
                var userId = User.GetUserId();
                var user = await _authService.GetCurrentUserAsync(userId);

                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Revoke refresh token
        /// </summary>
        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<IActionResult> RevokeToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest(new { message = "Refresh token not found." });
                }

                var result = await _authService.RevokeTokenAsync(refreshToken);

                if (!result)
                {
                    return BadRequest(new { message = "Invalid refresh token." });
                }

                Response.Cookies.Delete("refreshToken");
                return Ok(new { message = "Token revoked successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Revoke all refresh tokens for current user - Tự động lấy userId từ token
        /// </summary>
        [HttpPost("revoke-all-tokens")]
        [Authorize]
        public async Task<IActionResult> RevokeAllTokens()
        {
            try
            {
                // Tự động lấy userId từ token
                var userId = User.GetUserId();
                await _authService.RevokeAllTokensAsync(userId);

                Response.Cookies.Delete("refreshToken");
                return Ok(new { message = "All tokens revoked successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        #region Helper Methods

        private void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires,
                SameSite = SameSiteMode.Strict,
                Secure = true // Set to true in production with HTTPS
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        #endregion
    }
}
