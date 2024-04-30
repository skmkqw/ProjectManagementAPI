using System.Text.Json.Serialization;

namespace ProjectManagement.DataAccess.DTOs.Tasks;

public class UpdateTaskDto
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    [JsonIgnore]
    public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    
    public DateTime? Deadline { get; set; }
}