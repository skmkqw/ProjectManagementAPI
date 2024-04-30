using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.DataAccess.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}