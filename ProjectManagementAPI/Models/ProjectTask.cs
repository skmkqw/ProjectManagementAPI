using System.ComponentModel.DataAnnotations;

namespace ProjectManagementAPI.Models;

public class ProjectTask
{
    public int Id { get; set; }

    [Required] 
    public int ProjectId { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
}