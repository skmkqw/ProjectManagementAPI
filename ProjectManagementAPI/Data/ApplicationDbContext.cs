using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
        
    }
    
    public DbSet<Project> Projects { get; set; }
    
    public DbSet<ProjectTask> ProjectTasks { get; set; }

    public DbSet<User> Users { get; set; }

}