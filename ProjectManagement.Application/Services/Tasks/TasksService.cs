using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Mappers;
using ProjectManagement.DataAccess.Repositories.Tasks;

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

    public async Task<ProjectTask> GetTaskById(Guid id)
    {
        var taskEntity = await _tasksRepository.GetById(id);
        
        if (taskEntity == null)
        {
            throw new KeyNotFoundException("Task not found!");
        }

        return taskEntity.ToTaskModel();
    }

    #endregion GET METHODS
    
    
    #region PUT METHODS

    public async Task<ProjectTask> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
    {
        var taskEntity = await _tasksRepository.GetById(id);

        if (taskEntity == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        await _tasksRepository.Update(taskEntity, updateTaskDto);

        return taskEntity.ToTaskModel();
    }

    public async Task<ProjectTask> UpdateTaskStatus(Guid id, TaskStatuses status)
    {
        var taskEntity = await _tasksRepository.GetById(id);

        if (taskEntity == null)
        {
            throw new KeyNotFoundException("Task not found");

        }

        taskEntity.Status = status;

        await _tasksRepository.UpdateStatus(taskEntity);

        return taskEntity.ToTaskModel();
    }
    
    public async Task<ProjectTask> AssignUserToTask(Guid taskId, Guid userId)
    {
        try
        {
            var taskEntity = await _tasksRepository.AssignUser(taskId, userId);
            return taskEntity.ToTaskModel();
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    #endregion PUT METHODS
    
    
    #region DELETE METHODS

    public async Task DeleteTask(Guid id)
    {
        try
        {
            await _tasksRepository.Delete(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }

    public async Task RemoveUserFromTask(Guid taskId)
    {
        try
        {
            await _tasksRepository.RemoveUser(taskId);
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    #endregion DELETE METHODS
}