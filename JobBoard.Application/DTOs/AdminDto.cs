using System.ComponentModel.DataAnnotations;

namespace JobBoard.Application.DTOs;

public class AdminDto
{
  public required string Id { get; set; }
  
  public required string Name { get; set; }
  
  public required string Email { get; set; }
    
  public required bool Active { get; set; }
    
  public string? CompanyId { get; set; }
  
  public DateTime? CreatedAt { get; set; }
  
  public DateTime UpdatedAt { get; set; }
}

public class AdminCreateDto 
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public required string Email { get; set; }
    
    
    [Required(ErrorMessage = "CompanyId is required")]
    public required string CompanyId {  get; set; }
    
    // [Required(ErrorMessage = "RoleCode is required")]
    // public required string RoleCode {get; set;}
}

public class AdminUpdateDto
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Active is required")]
    public required bool Active { get; set; }
    
    [Required(ErrorMessage = "CompanyId is required")]
    public required string CompanyId {  get; set; }
}