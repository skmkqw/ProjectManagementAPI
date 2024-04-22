using ProjectManagement.Core.Models;

namespace ProjectManagement.DataAccess.Entities;

public class ProjectTaskEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }
    
    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;
    public ProjectEntity Project { get; set; }
}