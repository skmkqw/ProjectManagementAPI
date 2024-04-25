using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class ProjectUserMappers
{
    public static ProjectUser ToProjectUserModel(this ProjectUserEntity projectUserEntity)
    {
        return new ProjectUser()
        {
            ProjectId = projectUserEntity.ProjectId,
            Project = projectUserEntity.Project.ToProjectModel(),
            UserId = projectUserEntity.UserId,
            User = projectUserEntity.User.ToUserModel()
        };
    }
}