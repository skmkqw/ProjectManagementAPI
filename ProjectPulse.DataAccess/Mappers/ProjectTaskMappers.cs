using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectPulse.DataAccess.Mappers;

public static class ProjectTaskMappers
{
    public static ProjectTaskEntity FromCreateDtoToTaskEntity(this CreateTaskDto createTaskDto)
    {
        return new ProjectTaskEntity()
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            Deadline = createTaskDto.Deadline
        };
    }
    
    public static TaskDto ToTaskDto(this ProjectTask task)
    {
        return new TaskDto()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId,
            AssignedUserId = task.AssignedUserId,
            Status = task.Status,
            CreationDate = task.CreationDate,
            LastUpdateTime = task.LastUpdateTime,
            Deadline = task.Deadline
        };
    }
    
    public static ProjectTask ToTaskModel(this ProjectTaskEntity taskEntity)
    {
        return new ProjectTask()
        {
            Id = taskEntity.Id,
            Title = taskEntity.Title,
            Description = taskEntity.Description,
            ProjectId = taskEntity.ProjectId,
            AssignedUserId = taskEntity.AssignedUserId,
            Status = taskEntity.Status,
            CreationDate = taskEntity.CreationDate,
            LastUpdateTime = taskEntity.LastUpdateTime,
            Deadline = taskEntity.Deadline
        };
    }
}