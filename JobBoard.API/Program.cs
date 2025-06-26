
using JobBoard.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JobBoard.Application.Mappers;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Application.Interfaces.Repositories;

using JobBoard.API.Exceptions.Handlers;
using JobBoard.Infrastructure.Repositories;
using JobBoard.Infrastructure.Services;
using JobBoard.Infrastructure.Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//Config Swagger
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "JobBoard API";
});

//Config Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


//Add db configuration
builder.Services.AddDbContext<JobBoardDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Add services
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");

app.MapOpenApi();
app.UseOpenApi();
app.UseSwaggerUi();

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
