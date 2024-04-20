using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Entities;

public class ProjectTaskEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }
    
    public ProjectEntity Project { get; set; }
}