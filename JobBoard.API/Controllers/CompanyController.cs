using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/v1/companies")]
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
    public async Task<ActionResult<CompanyDto>> GetCompany(string id)
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
        return CreatedAtAction(nameof(GetCompany), created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(string id, UpdateCompanyDto dto)
    {
        var updated = await _companyService.UpdateAsync(id, dto);
        if (!updated)
            return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(string id)
    {
        var deleted = await _companyService.DeleteAsync(id);
        if(!deleted)
            return NotFound();
        return Ok(new{Id = id});
    }
}

