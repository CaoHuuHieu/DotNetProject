using JobBoard.API.Config;
using JobBoard.API.Controllers.Handlers;
using JobBoard.Infrastructure.Persistence;
using JobBoard.Application.Mappers;
using JobBoard.Application.Interfaces.Services;
using JobBoard.Application.Interfaces.Repositories;
using JobBoard.Infrastructure.Config;
using JobBoard.Infrastructure.Repositories;
using JobBoard.Infrastructure.Services;
using MongoDB.Driver;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//Config Logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["Logging:FileLocation"]!, rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

//Config Swagger
builder.Services.AddOpenApiDocument(openApiConfig =>
{   
    openApiConfig.Title = "JobBoard API";
    openApiConfig.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textbox: Bearer {your JWT token}."
    });
    
    openApiConfig.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

//Config Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));



// Config Authentication using Method extension
builder.Services.AddJwtAuthentication(config);

builder.Services.AddAuthorization();

//Add db configuration
builder.Services.AddDbContext<JobBoardDbContext>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IMongoDatabase>(cf =>
{
    var client = new MongoClient(config["MongoDB:ConnectionString"]);
    return client.GetDatabase(config["MongoDB:Database"]);
});

    
var app = builder.Build();


app.MapOpenApi();
app.UseAuthentication();
app.UseAuthorization();
app.UseOpenApi();
app.UseSwaggerUi();
app.UseExceptionHandler();
app.UseHttpsRedirection();


app.MapControllers();

app.Run();
