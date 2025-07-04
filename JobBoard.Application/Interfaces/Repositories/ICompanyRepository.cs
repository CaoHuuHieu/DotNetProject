using JobBoard.Application.DTOs;
using JobBoard.Domain.Entity;

namespace JobBoard.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<PageResponse<Company>> GetAllAsync(CompanyFilterDto request);
    Task<Company?> GetByIdAsync(string id);
    Task<Company> AddAsync(Company company);
    Task<bool> UpdateAsync(Company company);
    Task<bool> DeleteAsync(string id);
}
