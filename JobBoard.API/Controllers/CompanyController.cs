using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<CompanyDto>>> GetCompanies([FromQuery] CompanyFilterDto request)
    {
        var companies = await _companyService.GetAllAsync(request);
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
    {
        var company = await _companyService.GetByIdAsync(id);
        if (company == null)
            return NotFound();
        return Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto dto)
    {
        var created = await _companyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetCompany), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyDto dto)
    {
        var updated = await _companyService.UpdateAsync(id, dto);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var deleted = await _companyService.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}

