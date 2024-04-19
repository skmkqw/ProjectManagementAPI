using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Mappers;

public static class ProjectMappers
{
    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }

    public static Project ToProjectFromRequestDto(this ProjectFromRequestDto requestDto)
    {
        return new Project()
        {
            Name = requestDto.Name,
            Description = requestDto.Description
        };
    }
}