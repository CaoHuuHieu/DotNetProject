namespace JobBoard.Application.Interfaces.Services;

public interface IAuthService
{
    string GenerateJwtTokenAsync();
}