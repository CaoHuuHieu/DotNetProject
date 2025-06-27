using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Infrastructure.Sercurity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobBoard.Infrastructure.Services;

public class AuthService(IOptions<JwtSettings> options) : IAuthService
{
    private readonly JwtSettings _settings = options.Value;
    public string GenerateJwtTokenAsync()
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "username"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}