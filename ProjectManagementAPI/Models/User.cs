using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class User
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    public List<ProjectUser> ProjectUsers { get; set; } = new();
}