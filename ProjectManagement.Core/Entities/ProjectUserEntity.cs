namespace ProjectManagement.Core.Entities;

public class ProjectUserEntity
{
    public Guid ProjectId { get; set; }

    public ProjectEntity Project { get; set; } = null!;

    public Guid UserId { get; set; }

    public UserEntity User { get; set; } = null!;
}