using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.DataAccess.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public List<TaskDto>? Tasks { get; set; }
}