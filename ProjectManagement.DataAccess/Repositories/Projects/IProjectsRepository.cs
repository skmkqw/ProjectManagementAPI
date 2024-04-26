using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public interface IProjectsRepository
{
    public Task<IEnumerable<ProjectEntity>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectEntity projectEntity);

    public Task<IEnumerable<ProjectTaskEntity>> GetTasks(Guid projectId);

    public Task<ProjectTaskEntity> AddTask(Guid projectId, ProjectTaskEntity taskEntity);

    public Task<ProjectUserEntity> AddUser(Guid projectId, Guid userId);

    public Task<ProjectEntity?> Update(ProjectEntity projectEntity);

    public Task Delete(Guid id);
}