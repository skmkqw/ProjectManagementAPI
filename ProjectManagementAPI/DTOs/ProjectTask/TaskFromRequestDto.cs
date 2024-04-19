namespace ProjectManagementAPI.DTOs.ProjectTask;

public class TaskFromRequestDto
{
    public int ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}