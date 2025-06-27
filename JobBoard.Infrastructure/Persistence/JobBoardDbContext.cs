using JobBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobBoard.Infrastructure.Persistence;

public class JobBoardDbContext : DbContext
{
    
    private readonly IConfiguration _configuration;

    public JobBoardDbContext(IConfiguration configuration)
    {
        _configuration = configuration; 
    }
    public JobBoardDbContext(IConfiguration configuration, DbContextOptions<JobBoardDbContext> options)
        : base(options)
    {
        _configuration = configuration;    
    }

    public DbSet<Company> Companies => Set<Company>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobBoardDbContext).Assembly);
    }
    
}
