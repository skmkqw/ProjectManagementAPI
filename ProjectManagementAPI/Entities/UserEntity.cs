using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public List<ProjectUserEntity> ProjectUsers { get; set; } = new();
}