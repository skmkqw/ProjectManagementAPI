using Microsoft.AspNetCore.Identity;
using ProjectPulse.Core.Entities;

namespace ProjectPulse.Core.Models;

public class AppUser : IdentityUser<Guid>
{
    public IEnumerable<ProjectTaskEntity>? Tasks { get; set; }
    
    public List<ProjectUserEntity>? ProjectUsers { get; set; }

    public List<Project> Projects { get; set; } = new();
    
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    
    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
}