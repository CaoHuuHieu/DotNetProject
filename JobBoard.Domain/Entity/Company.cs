namespace JobBoard.Domain.Entity;

public class Company : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string Website { get; set; } = string.Empty;
    public required string Email { get; set; }
    public required string Address { get; set; }
    
}