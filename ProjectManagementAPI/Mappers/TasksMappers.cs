using ProjectManagementAPI.DTOs.ProjectTask;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Mappers;

public static class TasksMappers
{
    public static TaskDto ToTaskDto(this ProjectTask task)
    {
        return new TaskDto()
        {
            Id = task.Id,
            ProjectId = task.ProjectId,
            Title = task.Title,
            Description = task.Description
        };
    }

    public static ProjectTask ToTaskFromRequestDto(this TaskFromRequestDto requestDto)
    {
        return new ProjectTask()
        {
            ProjectId = requestDto.ProjectId,
            Title = requestDto.Title,
            Description = requestDto.Description
        };
    }
}