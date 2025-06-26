using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Domain.Entity;
using JobBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using JobBoard.Application.DTOs;
using System.Reflection;

namespace JobBoard.Infrastructure.Repositories;

public class CompanyRepository(JobBoardDbContext context) : AbstractRepository<Company>, ICompanyRepository
{
    public async Task<PageResponse<Company>> GetAllAsync(CompanyFilterDto request)
    {
        var query = context.Companies.AsQueryable();
        if (!string.IsNullOrEmpty(request.SearchBy) && !string.IsNullOrEmpty(request.SearchValue))
        {
            query = request.SearchBy.ToLower() switch
            {
                "name"  => query.Where(c => c.Name.Contains(request.SearchValue)),
                "code"  => query.Where(c => c.Code.Contains(request.SearchValue)),
                "email" => query.Where(c => c.Email.Contains(request.SearchValue)),
                _       => query
            };
        }

        return await ToPagedResultAsync(query, request);
    }

    public async Task<Company?> GetByIdAsync(Guid id)
    {
        return await context.Companies.FindAsync(id);
    }

    public async Task<Company> AddAsync(Company company)
    {
        context.Companies.Add(company);
        await context.SaveChangesAsync();
        return company;
    }

    public async Task<bool> UpdateAsync(Company company)
    {
        context.Companies.Update(company);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var company = await context.Companies.FindAsync(id);
        if (company == null) return false;
        context.Companies.Remove(company);
        return await context.SaveChangesAsync() > 0;
    }
}
