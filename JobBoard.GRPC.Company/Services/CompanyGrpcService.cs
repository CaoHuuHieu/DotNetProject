using Grpc.Core;
using GrpcCompany;
using JobBoard.Application.Interfaces.Services;

namespace JobBoard.GRPC.Company.Services;

public class CompanyGrpcServiceImpl : CompanyService.CompanyServiceBase
{
    private readonly ILogger<CompanyGrpcServiceImpl> _logger;
    private readonly ICompanyService _companyService;
    
    public CompanyGrpcServiceImpl(ILogger<CompanyGrpcServiceImpl> logger, ICompanyService companyService)
    {
        _logger = logger;
        _companyService = companyService;
    }
    public override async Task<CompanyReply> GetCompany(CompanyRequest request, ServerCallContext context)
    {
        _logger.LogDebug("[CompanyGrpcService][GetCompany] Request received for company ID: {Id}", request.Id);
        var company = await _companyService.GetByIdAsync(request.Id);
        if (company == null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Company with ID {request.Id} not found"));
        return new CompanyReply
        {
            Id = company.Id,
            Name = company.Name,
            Code = company.Code,
            Email = company.Email,
            Website = company.Website,
        };
    }
}