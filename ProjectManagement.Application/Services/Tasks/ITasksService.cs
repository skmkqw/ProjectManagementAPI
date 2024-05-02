using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.Application.Services.Tasks;

public interface ITasksService
{
    public Task<IEnumerable<ProjectTask>> GetAllTasks();

    public Task<ProjectTask> GetTaskById(Guid id);
    
    public Task<ProjectTask> AssignUserToTask(Guid taskId, Guid userId);

    public Task RemoveUserFromTask(Guid taskId);
    
    public Task<ProjectTask> UpdateTask(Guid id, UpdateTaskDto updateTaskDto);

    public Task<ProjectTask> UpdateTaskStatus(Guid id, TaskStatuses status);
    
    public Task<bool> DeleteTask(Guid id);
}