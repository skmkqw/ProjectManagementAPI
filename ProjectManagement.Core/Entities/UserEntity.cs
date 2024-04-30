namespace ProjectManagement.Core.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public List<ProjectTaskEntity> Tasks { get; set; } = new ();
    
    public List<ProjectUserEntity> ProjectUsers { get; set; } = new();
    
    public string Email { get; set; } = string.Empty;
    
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public DateTime EntryDate { get; set; } = DateTime.Now;
    
    public DateTime LastUpdateTime { get; set; } = DateTime.Now;
}