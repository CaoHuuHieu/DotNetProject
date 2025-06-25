using JobBoard.Domain.Entity;

namespace JobBoard.Application.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<Company?> GetByIdAsync(Guid id);
    Task<Company> AddAsync(Company company);
    Task<bool> UpdateAsync(Company company);
    Task<bool> DeleteAsync(Guid id);
}
