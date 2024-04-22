using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Mappers;

public static class UserMappers
{
    public static UserEntity FromDtoToUserEntity(this UserFromRequestDto userFromRequest)
    {
        return new UserEntity()
        {
            FirstName = userFromRequest.FirstName,
            LastName = userFromRequest.LastName
        };
    }
    
    public static User ToUserModel(this UserEntity userEntity)
    {
        return new User(userEntity.Id, userEntity.FirstName, userEntity.LastName);
    }
    
    public static UserEntity ToUserEntity(this User user)
    {
        return new UserEntity()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}