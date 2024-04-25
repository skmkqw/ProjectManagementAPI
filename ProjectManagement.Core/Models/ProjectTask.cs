namespace ProjectManagement.Core.Models;

public enum TaskStatuses
{
    ToDo,
    InProgress,
    Done
}

public class ProjectTask
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public Project? Project { get; set; }

    public Guid? AssignedUserId { get; set; }

    public User? AssignedUser { get; set; }
    
    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;
}