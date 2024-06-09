using ProjectPulse.Core.Entities;
using ProjectPulse.DataAccess.DTOs;

namespace ProjectPulse.DataAccess.Mappers;

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