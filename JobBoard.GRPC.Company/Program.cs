using GrpcCompany;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Application.Mappers;
using JobBoard.GRPC.Company.Services;
using JobBoard.Infrastructure.Persistence;
using JobBoard.Infrastructure.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<JobBoardDbContext>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, JobBoard.Infrastructure.Services.CompanyService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CompanyGrpcServiceImpl>();

app.MapGet("/", () => "Use a gRPC client to communicate with this server. For example, use the gRPC CLI tool or a gRPC client library in your application.");
app.Run();
