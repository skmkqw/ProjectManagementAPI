using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Projects;
using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectManagement.Application.Services.Projects;

public interface IProjectsService
{
    public Task<IEnumerable<Project>> GetAllProjects();
    
    public Task<Project?> GetProjectById(Guid id);
    
    public Task<IEnumerable<ProjectTask>?> GetProjectTasks(Guid projectId);
    
    public Task<IEnumerable<ProjectTask>?> GetUserTasks(Guid userId, Guid projectId);
    
    public Task<IEnumerable<AppUser>?> GetProjectUsers(Guid projectId);

    public Task<Project> CreateProject(CreateProjectDto createProjectDto, Guid creatorId);
    
    public Task<ProjectTask?> AddTask(Guid projectId, CreateTaskDto createTaskDto);
    
    public Task<(ProjectUserEntity? projectUserEntity, string? error)> AddUserToProject(Guid projectId, Guid userId);
    
    public Task<(Guid? userId, string? error)> RemoveUserFromProject(Guid projectId, Guid userId);
    
    public Task<Project?> UpdateProject(Guid id, UpdateProjectDto updateProjectDto);
    
    public Task<bool> DeleteProject(Guid id);
}