using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPulse.Core.Entities;

namespace ProjectPulse.DataAccess.Configurations;

public class ProjectsUsersConfiguration : IEntityTypeConfiguration<ProjectUserEntity>
{
    public void Configure(EntityTypeBuilder<ProjectUserEntity> builder)
    {
        builder.HasKey(pu => new { pu.ProjectId, pu.UserId });

        builder.HasOne(pu => pu.Project)
            .WithMany(p => p.ProjectUsers)
            .HasForeignKey(pu => pu.ProjectId);

        builder.HasOne(pu => pu.User)
            .WithMany(u => u.ProjectUsers)
            .HasForeignKey(pu => pu.UserId);
    }
}