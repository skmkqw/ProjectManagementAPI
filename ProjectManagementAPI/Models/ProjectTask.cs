using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class ProjectTask
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required] 
    public Guid ProjectId { get; set; }
    
    public Project Project { get; set; }
}