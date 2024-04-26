using ProjectManagement.Core.Models;

namespace ProjectManagement.Core.Entities;

public class ProjectTaskEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }
    
    public Guid? AssignedUserId { get; set; }

    public UserEntity? AssignedUser { get; set; }
    
    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;
}