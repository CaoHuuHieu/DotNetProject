using System.ComponentModel.DataAnnotations;

namespace JobBoard.Application.DTOs;

public class PasswordRequest
{
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Password is not valid")]
    public required string Password { get; set; }
}