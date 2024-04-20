using ProjectManagementAPI.DTOs.ProjectTask;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Tasks;

public interface ITasksRepository : IGenericRepository<ProjectTask>
{
    public Task<IEnumerable<ProjectTask>> GetByProjectId(Guid projectId);
    public Task<ProjectTask?> Create(TaskFromRequestDto taskFromRequestDto);

    public Task<ProjectTask?> Update(Guid id, TaskFromRequestDto taskFromRequestDto);
}