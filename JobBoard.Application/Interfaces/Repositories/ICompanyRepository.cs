using JobBoard.Application.DTOs;
using JobBoard.Domain.Entity;

namespace JobBoard.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync(CompanyFilterDto request);
    Task<Company?> GetByIdAsync(Guid id);
    Task<Company> AddAsync(Company company);
    Task<bool> UpdateAsync(Company company);
    Task<bool> DeleteAsync(Guid id);
}
