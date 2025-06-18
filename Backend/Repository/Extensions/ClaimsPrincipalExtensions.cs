using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                             ?? principal.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID in token.");
            }

            return userId;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value
                   ?? principal.FindFirst("email")?.Value
                   ?? throw new UnauthorizedAccessException("Email not found in token.");
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Role)?.Value
                   ?? principal.FindFirst("role")?.Value
                   ?? throw new UnauthorizedAccessException("Role not found in token.");
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name)?.Value
                   ?? throw new UnauthorizedAccessException("Name not found in token.");
        }
    }
}
