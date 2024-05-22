using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Tasks;

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