using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Core.Models;

namespace ProjectManagement.DataAccess.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(p => p.EntryDate)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.LastUpdateTime)
            .HasDefaultValueSql("GETDATE()");

        builder.HasMany(u => u.ProjectUsers)
            .WithOne(pu => pu.User)
            .HasForeignKey(pu => pu.UserId);
    }
}