using ProjectManagementAPI.DTOs.Tasks;
using ProjectManagementAPI.Entities;

namespace ProjectManagementAPI.Mappers;

public static class ProjectTaskMappers
{
    public static ProjectTaskEntity ToProjectTaskEntity(this ProjectTaskFromRequestDto projectTaskFromRequestDto)
    {
        return new ProjectTaskEntity()
        {
            Title = projectTaskFromRequestDto.Title,
            Description = projectTaskFromRequestDto.Description,
            ProjectId = projectTaskFromRequestDto.ProjectId
        };
    }
}