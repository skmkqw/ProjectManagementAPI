namespace ProjectManagement.Core.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public List<ProjectTaskEntity> Tasks { get; set; } = new ();
    public List<ProjectUserEntity> ProjectUsers { get; set; } = new();
}