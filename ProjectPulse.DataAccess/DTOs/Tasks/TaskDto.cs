using ProjectPulse.Core.Models;

namespace ProjectPulse.DataAccess.DTOs.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public Guid ProjectId { get; set; }
    
    public Guid? AssignedUserId { get; set; }
    
    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;

    public DateTime? Deadline { get; set; }
}