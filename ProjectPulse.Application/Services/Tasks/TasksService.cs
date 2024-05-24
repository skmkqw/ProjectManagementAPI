using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Tasks;
using ProjectPulse.DataAccess.Mappers;
using ProjectPulse.DataAccess.Repositories.Tasks;

namespace ProjectManagement.Application.Services.Tasks;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;

    public TasksService(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }
    
    #region GET METHODS

    public async Task<IEnumerable<ProjectTask>> GetAllTasks()
    {
        var taskEntities = await _tasksRepository.GetAll();
        return taskEntities.Select(t => t.ToTaskModel());
    }

    public async Task<ProjectTask?> GetTaskById(Guid id)
    {
        var taskEntity = await _tasksRepository.GetById(id);

        if (taskEntity == null) return null;

        return taskEntity.ToTaskModel();
    }

    #endregion GET METHODS
    
    
    #region PUT METHODS

    public async Task<ProjectTask?> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
    {
        var taskEntity = await _tasksRepository.GetById(id);

        if (taskEntity == null) return null;

        await _tasksRepository.Update(taskEntity, updateTaskDto);

        return taskEntity.ToTaskModel();
    }

    public async Task<(ProjectTask? task, string? error)> UpdateTaskStatus(Guid id, TaskStatuses status)
    {
        var taskEntity = await _tasksRepository.GetById(id);

        if (taskEntity == null)
        {
            return (null, "Task not found");
        }
        if (taskEntity.AssignedUserId == null)
        {
            return (null, "Can't change task status before assigning user!");
        }

        taskEntity.Status = status;
        taskEntity.LastUpdateTime = DateTime.UtcNow;

        await _tasksRepository.UpdateStatus(taskEntity);

        return (taskEntity.ToTaskModel(), null);
    }
    
    public async Task<(ProjectTask? task, string? error)> AssignUserToTask(Guid taskId, Guid userId)
    {
        (var task, string? error) = await _tasksRepository.AssignUser(taskId, userId);
        if (task != null)
        {
            return (task.ToTaskModel(), null);
        }
    
        return (null, error);
    }

    #endregion PUT METHODS
    
    
    #region DELETE METHODS

    public async Task<bool> DeleteTask(Guid id)
    {
        bool isDeleted = await _tasksRepository.Delete(id);
        return isDeleted;
    }

    public async Task<string?> RemoveUserFromTask(Guid taskId)
    {
        return await _tasksRepository.RemoveUser(taskId);
    }

    #endregion DELETE METHODS
}