using System.ComponentModel.DataAnnotations;

namespace JobBoard.Application.DTOs;

public class LoginRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }
}