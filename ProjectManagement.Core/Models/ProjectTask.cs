namespace ProjectManagement.Core.Models;

public class ProjectTask
{
    public ProjectTask(Guid id, string title, string description, Guid projectId)
    {
        Id = id;
        Title = title;
        Description = description;
        ProjectId = projectId;
    }
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public Guid ProjectId { get; set; }
    
    public Project Project { get; set; }

    public static ProjectTask Create(Guid id, string title, string description, Guid projectId)
    {
        return new ProjectTask(id, title, description, projectId);
    }
}