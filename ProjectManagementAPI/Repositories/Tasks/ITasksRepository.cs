using ProjectManagementAPI.DTOs.ProjectTask;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Tasks;

public interface ITasksRepository : IGenericRepository<ProjectTask>
{
    public Task<ProjectTask?> Create(TaskFromRequestDto taskFromRequestDto);

    public Task<ProjectTask?> Update(int id, TaskFromRequestDto taskFromRequestDto);
}