namespace ProjectManagementAPI.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}