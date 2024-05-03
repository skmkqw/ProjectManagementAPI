using System.Text.Json.Serialization;

namespace ProjectManagement.DataAccess.DTOs.Projects;

public class UpdateProjectDto
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [JsonIgnore]
    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
}