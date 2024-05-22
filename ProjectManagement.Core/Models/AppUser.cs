using Microsoft.AspNetCore.Identity;
using ProjectManagement.Core.Entities;

namespace ProjectManagement.Core.Models;

public class AppUser : IdentityUser<Guid>
{
    public IEnumerable<ProjectTaskEntity>? Tasks { get; set; }
    
    public List<ProjectUserEntity>? ProjectUsers { get; set; }

    public List<Project> Projects { get; set; } = new();
    
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    
    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
}