using System.ComponentModel.DataAnnotations;

namespace JobBoard.Application.DTOs;

public class CompanyDto
{
    public string Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Website { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class CreateCompanyDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "The name must not exceed 100 characters")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Code is required")]
    [StringLength(20, ErrorMessage = "The code must not exceed 10 characters")]
    public string Code { get; set; } = default!;

    [StringLength(100, ErrorMessage = "The website must not exceed 200 characters")]
    public string Website { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(50, ErrorMessage = "The email must not exceed 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(250, ErrorMessage = "The address must not exceed 250 characters")]
    public string Address { get; set; } = default!;
}

public class UpdateCompanyDto
{

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "The name must not exceed 100 characters")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Code is required")]
    [StringLength(20, ErrorMessage = "The code must not exceed 10 characters")]
    public string Code { get; set; } = default!;

    [StringLength(200, ErrorMessage = "The website must not exceed 200 characters")]
    public string Website { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(50, ErrorMessage = "The email must not exceed 50 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(250, ErrorMessage = "The address must not exceed 250 characters")]
    public string Address { get; set; } = default!;
}

public class CompanyFilterDto : PageRequestDto
{
   
}
