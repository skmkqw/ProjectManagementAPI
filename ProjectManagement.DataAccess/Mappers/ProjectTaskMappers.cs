using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectTaskMappers
{
    public static ProjectTaskEntity FromDtoToTaskEntity(this CreateTaskDto createTaskDto)
    {
        return new ProjectTaskEntity()
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
        };
    }
    
    public static ProjectTask ToTaskModel(this ProjectTaskEntity taskEntity)
    {
        return new ProjectTask(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.ProjectId, taskEntity.Status);
    }
    
    public static ProjectTaskEntity ToTaskEntity(this ProjectTask task)
    {
        return new ProjectTaskEntity()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ProjectId = task.ProjectId
        };
    }
}