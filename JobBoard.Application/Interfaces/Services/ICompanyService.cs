using JobBoard.Application.DTOs;

namespace JobBoard.Application.Interfaces.Services;

public interface ICompanyService
{
    Task<PageResponse<CompanyDto>> GetAllAsync(CompanyFilterDto request);
    Task<CompanyDto?> GetByIdAsync(string id);
    Task<CompanyDto> CreateAsync(CreateCompanyDto dto);
    Task<bool> UpdateAsync(string id, UpdateCompanyDto dto);
    Task<bool> DeleteAsync(string id);
}
