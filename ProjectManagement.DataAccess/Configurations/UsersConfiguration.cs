using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Core.Entities;

namespace ProjectManagement.DataAccess.Configurations;

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
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(25);
        
        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(20);
        
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