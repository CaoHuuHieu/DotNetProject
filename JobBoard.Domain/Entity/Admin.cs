
namespace JobBoard.Domain.Entity;

public class Admin : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Email { get; set; }
    
    // public required Role Role { get; set; }
    
    public required bool Active { get; set; }
    
    public string? CompanyId { get; set; }
}

public class Role
{
    public required string RoleCode { get; set; }
    public required string RoleName { get; set; }
}
