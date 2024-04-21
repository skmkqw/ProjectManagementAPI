namespace ProjectManagementAPI.DTOs.Tasks;

public class ProjectTaskFromRequestDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }
}