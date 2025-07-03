using AutoMapper;
using JobBoard.Application.DTOs;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Domain.Entity;
using Microsoft.Extensions.Logging;

namespace JobBoard.Infrastructure.Services;

public class AdminService : IAdminService
{
    
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IAdminRepository adminRepository, IMapper mapper, ILogger<AdminService> logger)
    {
        _adminRepository = adminRepository;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<PageResponse<AdminDto>> GetAllAdminsAsync(AdminRequestDto request)
    {
        var page = await _adminRepository.GetAllAdminsAsync(request);
        return new PageResponse<AdminDto>
        {
            TotalElements = page.TotalElements,
            CurrentPage = page.CurrentPage,
            TotalPages = page.TotalPages,
            PageSize = page.PageSize,
            Items = _mapper.Map<List<AdminDto>>(page.Items),
        };
    }

    public async Task<AdminDto?> GetAdminByIdAsync(string id)
    { 
        var admin = await _adminRepository.GetAdminByIdAsync(id);
        return  _mapper.Map<AdminDto>(admin);
    }

    public async Task<bool> CreateAdminAsync(AdminCreateDto request)
    {
        var admin = new Admin
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Email = request.Email,
            CompanyId = request.CompanyId
        };
        await _adminRepository.CreateAdminAsync(admin);
        return true;
    }

    public async Task<bool> UpdateAdminAsync(string id, AdminUpdateDto request)
    {
        var admin = await _adminRepository.GetAdminByIdAsync(id);
        if (admin == null)
            return false;
        admin.Name = request.Name;
        admin.Email = request.Email;
        admin.Active = request.Active;
        admin.CompanyId = request.CompanyId;
        await _adminRepository.UpdateAdminAsync(id, admin);
        return true;
    }

    public async Task DeleteAdminAsync(string id)
    {
        await _adminRepository.DeleteAdminAsync(id);
    }
}
