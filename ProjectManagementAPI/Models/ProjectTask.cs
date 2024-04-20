using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class ProjectTask
{
    public Guid Id { get; set; }

    [Required] 
    public Guid ProjectId { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
}