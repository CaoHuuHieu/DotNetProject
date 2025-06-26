using AutoMapper;
using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Domain.Entity;
using JobBoard.Application.Exceptions;

namespace JobBoard.Infrastructure.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;
    public CompanyService(ICompanyRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<PageResponse<CompanyDto>> GetAllAsync(CompanyFilterDto request)
    {
        var page = await _repository.GetAllAsync(request);
        return new PageResponse<CompanyDto>
        {
            TotalElements = page.TotalElements,
            CurrentPage = page.CurrentPage,
            TotalPages = page.TotalPages,
            PageSize = page.PageSize,
            Items = page.Items.Select(MapToDto)
        };
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
        if (company == null) 
            throw new NotFoundException($"Company with ID {id} not found.");
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

    private CompanyDto MapToDto(Company company)
    {
       return _mapper.Map<CompanyDto>(company);
    }
}
