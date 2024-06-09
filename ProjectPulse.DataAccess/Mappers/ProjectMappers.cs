using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Projects;

namespace ProjectPulse.DataAccess.Mappers;

public static class ProjectMappers
{
    public static ProjectEntity FromCreateDtoToProjectEntity(this CreateProjectDto createProjectDto)
    {
        return new ProjectEntity()
        {
            Name = createProjectDto.Name,
            Description = createProjectDto.Description,
        };
    }

    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreeationDate = project.CreationDate,
            LastUpdateTime = project.LastUpdateTime
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
            AddedUsers = projectEntity.ProjectUsers.Select(u => u.User).ToList(),
            CreationDate = projectEntity.CreationDate,
            LastUpdateTime = projectEntity.LastUpdateTime
        };
    }
}