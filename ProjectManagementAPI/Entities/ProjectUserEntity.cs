namespace ProjectManagementAPI.Entities;

public class ProjectUserEntity
{
    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; }

    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}