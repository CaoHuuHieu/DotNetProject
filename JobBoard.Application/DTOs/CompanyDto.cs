namespace JobBoard.Application.DTOs;

public class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Website { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class CreateCompanyDto
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Website { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
}

public class UpdateCompanyDto
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Website { get; set; } = string.Empty;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
}
