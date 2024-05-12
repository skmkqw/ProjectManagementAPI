using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public interface IProjectsRepository
{
    public Task<IEnumerable<ProjectEntity>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<IEnumerable<ProjectTaskEntity>?> GetTasks(Guid projectId);
    
    public Task<IEnumerable<AppUser>?> GetUsers(Guid projectId);

    public Task<ProjectEntity> Create(ProjectEntity projectEntity);

    public Task<ProjectTaskEntity?> AddTask(Guid projectId, ProjectTaskEntity taskEntity);
    
    public Task<(ProjectUserEntity? userEntity, string? error)> AddUser(Guid projectId, Guid userId);

    public Task<(Guid? userId, string? error)> RemoveUser(Guid projectId, Guid userId);

    public Task<ProjectEntity> Update(ProjectEntity projectEntity, UpdateProjectDto updateProjectDto);

    public Task<bool> Delete(Guid id);
}