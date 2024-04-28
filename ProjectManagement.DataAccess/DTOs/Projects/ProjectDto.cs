using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.DTOs.Projects;

public class ProjectDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}