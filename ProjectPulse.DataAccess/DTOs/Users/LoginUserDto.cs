using System.ComponentModel.DataAnnotations;

namespace ProjectPulse.DataAccess.DTOs.Users;

public class LoginUserDto
{
    [Required(ErrorMessage = "User name is required")]
    public string UserName { get; set; } = string.Empty; 
		
    [Required(ErrorMessage = "Password is required")] 
    public string Password { get; set; } = string.Empty;
}