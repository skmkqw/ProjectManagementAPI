using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Entities;

namespace ProjectManagementAPI.Mappers;

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