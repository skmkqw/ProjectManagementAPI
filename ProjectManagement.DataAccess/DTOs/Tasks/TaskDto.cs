using ProjectManagement.Core.Models;

namespace ProjectManagement.DataAccess.DTOs.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public Guid ProjectId { get; set; }
    
    public Guid? AssignedUserId { get; set; }
    
    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;
}