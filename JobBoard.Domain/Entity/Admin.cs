
namespace JobBoard.Domain.Entity;

public class Admin : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Email { get; set; }
    
    // public required Role Role { get; set; }

    public bool Active { get; set; } = false;
    
    public string Password { get; set; } = string.Empty;
    
    public string? CompanyId { get; set; }
}

public class Role
{
    public required string RoleCode { get; set; }
    public required string RoleName { get; set; }
}
