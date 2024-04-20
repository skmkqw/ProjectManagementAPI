using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class Project
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}