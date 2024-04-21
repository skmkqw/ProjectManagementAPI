using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

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