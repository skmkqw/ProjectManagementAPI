using ProjectManagementAPI.DTOs.Tasks;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Tasks;

public interface ITasksRepository
{
    public Task<IEnumerable<ProjectTask>> GetAll();

    public Task<ProjectTaskEntity?> GetById(Guid id);

    public Task<IEnumerable<ProjectTask>> GetByProjectId(Guid projectId);
    
    public Task<ProjectTaskEntity?> Create(ProjectTaskFromRequestDto projectTaskFromRequest);

    public Task<ProjectTaskEntity?> Update(Guid id, ProjectTaskFromRequestDto projectTaskFromRequest);

    public Task<int> Delete(Guid id);
}