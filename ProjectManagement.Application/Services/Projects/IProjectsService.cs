using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;

namespace ProjectManagement.Application.Services.Projects;

public interface IProjectsService
{
    public Task<IEnumerable<Project>> GetAllProjects();

    public Task<Project> GetProjectById(Guid id);

    public Task<Project> CreateProject(ProjectFromRequestDto projectFromRequestDto);
    
    public Task<Project> UpdateProject(Guid id, ProjectFromRequestDto projectFromRequestDto);
    
    public Task DeleteProject(Guid id);
}