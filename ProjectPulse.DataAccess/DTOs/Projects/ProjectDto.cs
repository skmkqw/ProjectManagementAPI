namespace ProjectPulse.DataAccess.DTOs.Projects;

public class ProjectDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateTime CreeationDate { get; set; }
    
    public DateTime LastUpdateTime { get; set; }

}