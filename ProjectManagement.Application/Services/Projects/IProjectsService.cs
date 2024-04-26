using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.Application.Services.Projects;

public interface IProjectsService
{
    public Task<IEnumerable<Project>> GetAllProjects();
    
    public Task<Project> GetProjectById(Guid id);
    
    public Task<IEnumerable<ProjectTask>> GetTasks(Guid projectId);
    
    public Task<IEnumerable<User>> GetUsers(Guid projectId);

    public Task<Project> CreateProject(ProjectFromRequestDto projectFromRequestDto);
    
    public Task<ProjectTask> AddTask(Guid projectId, CreateTaskDto createTaskDto);
    
    public Task<ProjectUserEntity> AddUser(Guid projectId, Guid userId);
    
    public Task RemoveUser(Guid projectId, Guid userId);
    
    public Task<Project> UpdateProject(Guid id, ProjectFromRequestDto projectFromRequestDto);
    
    public Task DeleteProject(Guid id);
}