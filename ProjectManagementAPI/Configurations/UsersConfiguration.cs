using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagementAPI.Entities;

namespace ProjectManagementAPI.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);

        builder.HasMany(u => u.ProjectUsers)
            .WithOne(pu => pu.User)
            .HasForeignKey(pu => pu.UserId)
            .IsRequired();
    }
}