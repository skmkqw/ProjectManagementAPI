namespace ProjectManagementAPI.DTOs.ProjectTask;

public class TaskDto
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}