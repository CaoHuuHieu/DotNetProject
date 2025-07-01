using JobBoard.Application.DTOs;

namespace JobBoard.Application.Interfaces.Services;

public interface IAuthService
{
    
    Task<TokenResponse> GenerateJwtTokenAsync(LoginRequest request);

    Task<bool> SetPassword(LoginRequest request);
    
}