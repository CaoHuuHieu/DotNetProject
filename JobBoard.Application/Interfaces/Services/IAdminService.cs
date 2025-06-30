using JobBoard.Application.DTOs;

namespace JobBoard.Application.Interfaces.Services;

public interface IAdminService
{
    Task<PageResponse<AdminDto>> GetAllAdminsAsync(AdminRequestDto request);

    Task<AdminDto?> GetAdminByIdAsync(string id);

    Task<bool> CreateAdminAsync(AdminCreateDto user);

    Task<bool> UpdateAdminAsync(string id, AdminUpdateDto user);

    Task DeleteAdminAsync(string id);
}