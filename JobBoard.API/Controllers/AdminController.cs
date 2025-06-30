using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/v1/admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<AdminDto>>> GetCompanies([FromQuery] AdminRequestDto request)
    {
        var admins = await _adminService.GetAllAdminsAsync(request);
        return Ok(admins);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetAdminByIdAsync(string id)
    {
        var company = await _adminService.GetAdminByIdAsync(id);
        if (company == null)
            return NotFound();
        return Ok(company);
    }

    [HttpPost]
    public async Task<ActionResult<AdminDto>> CreateCompany(AdminCreateDto dto)
    {
        await _adminService.CreateAdminAsync(dto);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(string id, AdminUpdateDto dto)
    {
        var updated = await _adminService.UpdateAdminAsync(id, dto);
        if (!updated)
            return NotFound();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(string id)
    {
        await _adminService.DeleteAdminAsync(id);
        return Ok();
    }
}