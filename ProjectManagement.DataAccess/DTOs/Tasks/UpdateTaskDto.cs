namespace ProjectManagement.DataAccess.DTOs.Tasks;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime? Deadline { get; set; }
}