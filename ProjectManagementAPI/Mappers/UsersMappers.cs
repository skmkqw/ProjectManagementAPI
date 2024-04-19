using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Mappers;

public static class UsersMappers
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public static User ToUserFromRequestDto(this UserFromRequestDto requestDto)
    {
        return new User()
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName
        };
    }
}