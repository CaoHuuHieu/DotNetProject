using AutoMapper;
using JobBoard.Domain.Entity;
using JobBoard.Application.DTOs;

namespace JobBoard.Application.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();
    }
}