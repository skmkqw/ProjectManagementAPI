using ProjectManagement.Core.Models;

namespace ProjectManagement.Core.Entities;

public class ProjectUserEntity
{
    public Guid ProjectId { get; set; }

    public ProjectEntity Project { get; set; } = null!;

    public Guid UserId { get; set; }

    public AppUser User { get; set; } = null!;
}