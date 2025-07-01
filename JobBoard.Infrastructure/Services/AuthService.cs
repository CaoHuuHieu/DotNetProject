using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Isopoh.Cryptography.Argon2;
using JobBoard.API.Config;
using JobBoard.Application.DTOs;
using JobBoard.Application.Exceptions;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobBoard.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IAdminRepository _adminRepository;
    
    public AuthService(IOptions<JwtSettings> jwtSettings, IAdminRepository adminRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _adminRepository = adminRepository;
    }
    public async Task<TokenResponse> GenerateJwtTokenAsync(LoginRequest request)
    {
        var admin = await _adminRepository.GetAdminByEmailAsync(request.Username);
        if (admin is not { Active: true } || !Argon2.Verify(admin?.Password, request.Password))
            throw new BusinessException((int)HttpStatusCode.Unauthorized, "Invalid username or password.");
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, admin!.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenConfig = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        string token = new JwtSecurityTokenHandler().WriteToken(tokenConfig);
        return new TokenResponse
        {
            Token = token,
            Expiration = (int)TimeSpan.FromMinutes(30).TotalSeconds,
        };
    }

    public async Task<bool> SetPassword(LoginRequest request)
    {
        var admin = await _adminRepository.GetAdminByEmailAsync(request.Username);
        if (admin == null || admin.Active == true)
            throw new BusinessException((int)HttpStatusCode.BadRequest, "Admin not found or already active.");
        admin.Password = Argon2.Hash(request.Password);
        admin.Active = true;
        await _adminRepository.UpdateAdminAsync(admin.Id, admin);
        return true;
    }
}