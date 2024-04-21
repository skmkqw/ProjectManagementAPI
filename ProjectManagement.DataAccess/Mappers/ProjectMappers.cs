using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

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