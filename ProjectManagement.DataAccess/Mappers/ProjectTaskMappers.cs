using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectTaskMappers
{
    public static ProjectTaskEntity FromDtoToTaskEntity(this ProjectTaskFromRequestDto projectTaskFromRequestDto)
    {
        return new ProjectTaskEntity()
        {
            Title = projectTaskFromRequestDto.Title,
            Description = projectTaskFromRequestDto.Description,
            ProjectId = projectTaskFromRequestDto.ProjectId
        };
    }
    
    public static ProjectTask ToTaskModel(this ProjectTaskEntity taskEntity)
    {
        return new ProjectTask(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.ProjectId);
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