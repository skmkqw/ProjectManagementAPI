using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementAPI.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required] 
    public string LastName { get; set; } = string.Empty;
}