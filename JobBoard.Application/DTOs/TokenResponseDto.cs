namespace JobBoard.Application.DTOs;

public class TokenResponse
{
    public required string Token { get; init; }
    public required int Expiration { get; init; }
}