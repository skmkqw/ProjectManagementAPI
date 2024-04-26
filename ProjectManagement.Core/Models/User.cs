namespace ProjectManagement.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public List<ProjectTask>? Tasks { get; set; }

    public List<Project> Projects { get; set; } = new();
}