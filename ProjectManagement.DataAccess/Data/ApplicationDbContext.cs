using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Configurations;

namespace ProjectManagement.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }
    
    public DbSet<ProjectEntity> Projects { get; set; }
    
    public DbSet<ProjectTaskEntity> ProjectTasks { get; set; }
    
    public DbSet<ProjectUserEntity> ProjectUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProjectsConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTasksConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectsUsersConfiguration());
    }
}