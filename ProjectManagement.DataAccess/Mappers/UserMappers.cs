using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class UserMappers
{
    public static UserEntity ToUserEntity(this UserFromRequestDto userFromRequest)
    {
        return new UserEntity()
        {
            FirstName = userFromRequest.FirstName,
            LastName = userFromRequest.LastName
        };
    }
}