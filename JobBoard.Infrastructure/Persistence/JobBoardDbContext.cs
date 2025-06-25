using JobBoard.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Persistence;

public class JobBoardDbContext : DbContext
{
    public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies => Set<Company>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobBoardDbContext).Assembly);
    }
    
}
