using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public JwtTokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

        // Sử dụng cách khác để get config value
        var tokenExpiryHours = 1; // default value
        if (int.TryParse(_configuration["Jwt:ExpiryHours"], out var configHours))
        {
            tokenExpiryHours = configHours;
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(tokenExpiryHours),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<User?> GetUserFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jsonToken?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            return await _unitOfWork.Users.GetByIdAsync(userId);
        }
        catch
        {
            return null;
        }
    }
}
