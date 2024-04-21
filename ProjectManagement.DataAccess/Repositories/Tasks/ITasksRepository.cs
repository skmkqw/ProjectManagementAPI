using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Tasks;

public interface ITasksRepository
{
    public Task<IEnumerable<ProjectTask>> GetAll();

    public Task<ProjectTaskEntity?> GetById(Guid id);

    public Task<IEnumerable<ProjectTask>> GetByProjectId(Guid projectId);
    
    public Task<ProjectTaskEntity?> Create(ProjectTaskFromRequestDto projectTaskFromRequest);

    public Task<ProjectTaskEntity?> Update(Guid id, ProjectTaskFromRequestDto projectTaskFromRequest);

    public Task<int> Delete(Guid id);
}