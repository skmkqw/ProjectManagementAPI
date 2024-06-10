using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPulse.Core.Entities;

namespace ProjectPulse.DataAccess.Configurations;

public class ProjectTasksConfiguration : IEntityTypeConfiguration<ProjectTaskEntity>
{
    public void Configure(EntityTypeBuilder<ProjectTaskEntity> builder)
    {
        builder.ToTable("ProjectTasks");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(250);

        builder.Property(t => t.Description)
            .IsUnicode()
            .HasMaxLength(500);

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(u => u.AssignedUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(u => u.AssignedUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.Status)
            .IsRequired();
        
        builder.Property(t => t.Priority)
            .IsRequired();
        
        builder.Property(p => p.CreationDate)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.LastUpdateTime)
            .HasDefaultValueSql("GETDATE()");
    }
}