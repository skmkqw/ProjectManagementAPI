using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Configurations;

namespace ProjectManagementAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
    }
    
    public DbSet<ProjectEntity> Projects { get; set; }
    
    public DbSet<ProjectTaskEntity> ProjectTasks { get; set; }

    public DbSet<UserEntity> Users { get; set; }
    
    
    public DbSet<ProjectUserEntity> ProjectUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectsConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTasksConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectsUsersConfiguration());
    }

}