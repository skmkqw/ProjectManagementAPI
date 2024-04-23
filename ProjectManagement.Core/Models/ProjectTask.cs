namespace ProjectManagement.Core.Models;

public enum TaskStatuses
{
    ToDo,
    InProgress,
    Done
}

public class ProjectTask
{
    public ProjectTask(Guid id, string title, string description, Guid projectId, TaskStatuses status = 0)
    {
        Id = id;
        Title = title;
        Description = description;
        ProjectId = projectId;

        if (status != null)
        {
            Status = status;
        }
    }
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public Guid ProjectId { get; set; }
    
    public Project Project { get; set; }

    public TaskStatuses Status { get; set; } = TaskStatuses.ToDo;

    public static ProjectTask Create(Guid id, string title, string description, Guid projectId)
    {
        return new ProjectTask(id, title, description, projectId);
    }
}