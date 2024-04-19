namespace ProjectManagementAPI.Models;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<ProjectTask> Tasks { get; set; } = new();
}