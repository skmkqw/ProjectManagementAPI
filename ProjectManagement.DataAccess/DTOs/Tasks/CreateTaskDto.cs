namespace ProjectManagement.DataAccess.DTOs.Tasks;

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateTime? Deadline { get; set; } = null;
}