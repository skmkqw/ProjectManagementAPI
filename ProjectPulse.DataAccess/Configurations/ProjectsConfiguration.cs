using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPulse.Core.Entities;

namespace ProjectPulse.DataAccess.Configurations;

public class ProjectsConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Projects");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name).IsRequired()
            .IsUnicode()
            .HasMaxLength(250);
        
        builder.Property(p => p.Description)
            .IsUnicode()
            .HasMaxLength(500);

        builder.Property(p => p.CreationDate)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.LastUpdateTime)
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProjectUsers)
            .WithOne(pu => pu.Project)
            .HasForeignKey(pu => pu.ProjectId);
    }
}