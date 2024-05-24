namespace ProjectPulse.Core.Entities;

public class ProjectEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public List<ProjectTaskEntity> Tasks { get; set; } = new();

    public List<ProjectUserEntity> ProjectUsers { get; set; } = new();
    
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
}