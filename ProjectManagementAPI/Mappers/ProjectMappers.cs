using ProjectManagementAPI.DTOs.Projects;
using ProjectManagementAPI.Entities;

namespace ProjectManagementAPI.Mappers;

public static class ProjectMappers
{
    public static ProjectEntity ToProjectEntity(this ProjectFromRequestDto projectFromRequestDto)
    {
        return new ProjectEntity()
        {
            Name = projectFromRequestDto.Name,
            Description = projectFromRequestDto.Description
        };
    }
}