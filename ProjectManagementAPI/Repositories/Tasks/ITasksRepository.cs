using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Tasks;

public interface ITasksRepository
{
    public Task<IEnumerable<ProjectTask>> GetAll();

    public Task<ProjectTask?> GetById(Guid id);

    public Task<IEnumerable<ProjectTask>> GetByProjectId(Guid projectId);
    
    public Task<ProjectTaskEntity?> Create(ProjectTaskEntity projectTaskEntityEntity);

    public Task<ProjectTaskEntity?> Update(Guid id, ProjectTaskEntity projectTaskEntity);

    public Task<int> Delete(Guid id);
}