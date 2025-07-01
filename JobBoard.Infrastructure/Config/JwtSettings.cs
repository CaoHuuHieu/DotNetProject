namespace JobBoard.API.Config;

public class JwtSettings
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string Key { get; init; }
}