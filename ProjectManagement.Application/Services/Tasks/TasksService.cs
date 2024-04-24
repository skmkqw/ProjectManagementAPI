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
    
    public async Task<IEnumerable<ProjectTask>> GetAllTasks()
    {
        var taskEntities = await _tasksRepository.GetAll();
        
        List<ProjectTask> tasks = new();
        foreach (var taskEntity in taskEntities)
        {
            tasks.Add(taskEntity.ToTaskModel());
        }

        return tasks;
    }

    public async Task<ProjectTask> GetTasktById(Guid id)
    {
        var taskEntity = await _tasksRepository.GetById(id);
        
        if (taskEntity == null)
        {
            return null;
        }

        return taskEntity.ToTaskModel();
    }

    public async Task<IEnumerable<ProjectTask>> GetTasktByProjectId(Guid projectId)
    {
        var taskEntities = await _tasksRepository.GetByProjectId(projectId);
        
        List<ProjectTask> tasks = new();
        foreach (var taskEntity in taskEntities)
        {
            tasks.Add(taskEntity.ToTaskModel());
        }

        return tasks;
    }

    public async Task<ProjectTask> UpdateTask(Guid id, UpdateTaskDto updateTaskDto)
    {
        var taskEntity = await _tasksRepository.GetById(id);
        
        if (taskEntity == null)
            throw new ArgumentException("Task not found");

        taskEntity.Title = updateTaskDto.Title;
        taskEntity.Description = updateTaskDto.Description;

        await _tasksRepository.Update(taskEntity);

        return taskEntity.ToTaskModel();
    }

    public async Task<ProjectTask> UpdateTaskStatus(Guid id, TaskStatuses status)
    {
        var taskEntity = await _tasksRepository.GetById(id);
        
        if (taskEntity == null)
            throw new ArgumentException("Task not found");

        taskEntity.Status = status;

        await _tasksRepository.Update(taskEntity);

        return taskEntity.ToTaskModel();
    }

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
}