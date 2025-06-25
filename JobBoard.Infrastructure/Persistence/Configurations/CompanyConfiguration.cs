
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBoard.Infrastructure.Persistence.Configurations; 

public class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entity.Company>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Company> builder)
    {
        builder.ToTable("companies");

        // builder.HasKey(c => c.Id);
        // builder.HasIndex(c => c.Code)
        //     .IsUnique();
        // builder.HasIndex(c => c.Email)
        //     .IsUnique();

        // builder.Property(c => c.Name)
        //     .IsRequired()
        //     .HasMaxLength(100);

        // builder.Property(c => c.Code)
        //     .IsRequired()
        //     .HasMaxLength(10);    

        // builder.Property(c => c.Website)
        //     .HasMaxLength(200);

        // builder.Property(c => c.Email)
        //     .IsRequired()
        //     .HasMaxLength(50);

        // builder.Property(c => c.Address)
        //     .IsRequired()
        //     .HasMaxLength(250);
    }
}   
