using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.Mappers;

public static class UserMappers
{
    public static UserEntity FromCreateDtoToUserEntity(this CreateUserDto createUser)
    {
        return new UserEntity()
        {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            Email = createUser.Email,
            Login = createUser.Login,
            Password = createUser.Password
        };
    }
    
    public static UserDto FromUserModelToDto(this User user)
    {
        
        return new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Login = user.Login,
            Password = user.Password
        };
    }
    
    public static User ToUserModel(this UserEntity userEntity)
    {
        return new User()
        {
            Id = userEntity.Id,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            Email = userEntity.Email,
            Login = userEntity.Login,
            Password = userEntity.Password
        };
    }
}