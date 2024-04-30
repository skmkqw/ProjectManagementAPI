namespace ProjectManagement.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public List<ProjectTask>? Tasks { get; set; }

    public List<Project> Projects { get; set; } = new();

    public string Email { get; set; } = string.Empty;
    
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

}