using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.DTOs;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectUsersMappers
{
    public static ProjectUsersDto ToDto(this ProjectUserEntity projectUserEntity)
    {
        return new ProjectUsersDto()
        {
            ProjectId = projectUserEntity.ProjectId,
            UserId = projectUserEntity.UserId
        };
    }
}