using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectMappers
{
    public static ProjectEntity FromCreateDtoToProjectEntity(this CreateProjectDto createProjectDto)
    {
        return new ProjectEntity()
        {
            Name = createProjectDto.Name,
            Description = createProjectDto.Description
        };
    }

    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
        };
    }

    public static Project ToProjectModel(this ProjectEntity projectEntity)
    {
        return new Project()
        {
            Id = projectEntity.Id, 
            Name = projectEntity.Name, 
            Description = projectEntity.Description,
            Tasks = projectEntity.Tasks.Select(p => p.ToTaskModel()).ToList(),
            AddedUsers = projectEntity.ProjectUsers.Select(u => u.User.ToUserModel()).ToList()
        };
    }
}