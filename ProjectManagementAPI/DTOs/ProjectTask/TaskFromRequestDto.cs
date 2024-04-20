namespace ProjectManagementAPI.DTOs.ProjectTask;

public class TaskFromRequestDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}