namespace ProjectManagement.Core.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<ProjectTask> Tasks { get; set; } = new();
    public List<ProjectUser> ProjectUsers { get; set; } = new();
}