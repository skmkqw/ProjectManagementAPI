using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public interface IProjectsRepository
{
    public Task<IEnumerable<ProjectEntity>> GetAll();

    public Task<ProjectEntity?> GetById(Guid id);
    
    public Task<ProjectEntity?> Create(ProjectEntity projectEntity);

    public Task<ProjectTaskEntity> AddTask(Guid projectId, ProjectTaskEntity taskEntity);

    public Task<ProjectEntity?> Update(ProjectEntity projectEntity);

    public Task Delete(Guid id);
}