using ProjectManagement.Core.Models;

namespace ProjectManagement.DataAccess.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public List<ProjectTaskEntity>? Tasks { get; set; }
    public List<ProjectUserEntity>? ProjectUsers { get; set; }
}