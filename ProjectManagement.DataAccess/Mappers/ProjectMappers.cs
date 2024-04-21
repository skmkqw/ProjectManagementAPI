using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectMappers
{
    public static ProjectEntity FromDtoToProjectEntity(this ProjectFromRequestDto projectFromRequestDto)
    {
        return new ProjectEntity()
        {
            Name = projectFromRequestDto.Name,
            Description = projectFromRequestDto.Description
        };
    }

    public static Project ToProjectModel(this ProjectEntity projectEntity)
    {
        return new Project(projectEntity.Id, projectEntity.Name, projectEntity.Description);
    }
    
    public static ProjectEntity ToProjectEntity(this Project project)
    {
        return new ProjectEntity()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description
        };
    }
}