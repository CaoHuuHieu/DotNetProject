using JobBoard.Application.DTOs;
using JobBoard.Domain.Entity;

namespace JobBoard.Application.Interfaces.Repositories;

public interface IAdminRepository
{
    Task<PageResponse<Admin>> GetAllAdminsAsync(AdminRequestDto request);

    Task<Admin?> GetAdminByIdAsync(string id);

    Task CreateAdminAsync(Admin user);

    Task UpdateAdminAsync(string id, Admin user);
    
    Task DeleteAdminAsync(string id);
    
    Task<Admin?> GetAdminByEmailAsync(string requestUsername);
}