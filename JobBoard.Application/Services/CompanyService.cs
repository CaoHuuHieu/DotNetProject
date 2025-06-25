using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces;
using JobBoard.Domain.Entity;

namespace JobBoard.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var companies = await _repository.GetAllAsync();
        return companies.Select(MapToDto);
    }

    public async Task<CompanyDto?> GetByIdAsync(Guid id)
    {
        var company = await _repository.GetByIdAsync(id);
        return company == null ? null : MapToDto(company);
    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Code = dto.Code,
            Website = dto.Website,
            Email = dto.Email,
            Address = dto.Address
        };
        var created = await _repository.AddAsync(company);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCompanyDto dto)
    {
        var company = await _repository.GetByIdAsync(id);
        if (company == null) return false;
        company.Name = dto.Name;
        company.Code = dto.Code;
        company.Website = dto.Website;
        company.Email = dto.Email;
        company.Address = dto.Address;
        return await _repository.UpdateAsync(company);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static CompanyDto MapToDto(Company company) => new CompanyDto
    {
        Id = company.Id,
        Name = company.Name,
        Code = company.Code,
        Website = company.Website,
        Email = company.Email,
        Address = company.Address
    };
}
