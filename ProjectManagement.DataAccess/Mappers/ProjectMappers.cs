using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;

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

    public static ProjectDto FromProjectModelToDto(this Project project)
    {
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Tasks = project.Tasks.Select(t => t.FromTaskModelToDto()).ToList(),
            AddedUsers = project.AddedUsers.Select(u => u.FromUserModelToDto()).ToList()
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