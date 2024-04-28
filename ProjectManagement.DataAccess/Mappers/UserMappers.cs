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
            LastName = createUser.LastName
        };
    }
    
    public static UserDto FromUserModelToDto(this User user)
    {
        if (user.Tasks != null)
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Tasks = user.Tasks.Select(t => t.ToTaskDto()).ToList(),
            };
        return new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };
    }
    
    public static User ToUserModel(this UserEntity userEntity)
    {
        if (userEntity.Tasks != null)
        {
            return new User()
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Tasks = userEntity.Tasks.Select(t => t.ToTaskModel()).ToList()
            };
        }

        return new User()
        {
            Id = userEntity.Id,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
        };
    }
}