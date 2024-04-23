using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.Application.Services.Tasks;

public interface ITasksService
{
    public Task<IEnumerable<ProjectTask>> GetAllTasks();

    public Task<ProjectTask> GetTasktById(Guid id);
    
    public Task<IEnumerable<ProjectTask>> GetTasktByProjectId(Guid projectId);

    public Task<ProjectTask> CreateTask(ProjectTaskFromRequestDto taskFromRequestDto);
    
    public Task<ProjectTask> UpdateTask(Guid id, ProjectTaskFromRequestDto taskFromRequestDto);

    public Task<ProjectTask> UpdateTaskStatus(Guid id, TaskStatuses status);
    
    public Task DeleteTask(Guid id);
}