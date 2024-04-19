using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    public string LastName { get; set; } = string.Empty;
}