using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyService.GetAllAsync();
            return Ok(companies);
        }

        // GET: api/Company/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await _companyService.GetByIdAsync(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto dto)
        {
            var created = await _companyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCompany), new { id = created.Id }, created);
        }

        // PUT: api/Company/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyDto dto)
        {
            var updated = await _companyService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/Company/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var deleted = await _companyService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
