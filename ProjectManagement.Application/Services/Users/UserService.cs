using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Entities;
using ProjectManagement.DataAccess.Mappers;
using ProjectManagement.DataAccess.Repositories.Users;

namespace ProjectManagement.Application.Services.Users;

public class UserService : IUsersService
{
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var userEntities = await _usersRepository.GetAll();
        
        List<User> users = new();
        foreach (UserEntity userEntity in userEntities)
        {
            users.Add(userEntity.ToUserModel());
        }

        return users;
    }

    public async Task<User> GetUserById(Guid id)
    {
        var userEntity = await _usersRepository.GetById(id);
        
        if (userEntity == null)
        {
            return null;
        }

        return userEntity.ToUserModel();
    }

    public async Task<User> CreateUser(UserFromRequestDto userFromRequest)
    {
        var userEntity = userFromRequest.FromDtoToUserEntity();

        var createdEntity = await _usersRepository.Create(userEntity);

        return createdEntity.ToUserModel();
    }

    public async Task<User> UpdateUser(Guid id, UserFromRequestDto userFromRequest)
    {
        var userEntity = await _usersRepository.GetById(id);
        
        if (userEntity == null)
            throw new ArgumentException("AssignedUser not found");

        userEntity.FirstName = userFromRequest.FirstName;
        userEntity.LastName = userFromRequest.LastName;

        await _usersRepository.Update(userEntity);

        return userEntity.ToUserModel();
    }

    public async Task<IEnumerable<ProjectTask>> GetTasks(Guid userId)
    {
        try
        {
            var taskEntities = await _usersRepository.GetTasks(userId);
            var tasks = taskEntities.Select(t => t.ToTaskModel());
            return tasks;
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    public async Task DeleteUser(Guid id)
    {
        try
        {
            await _usersRepository.Delete(id);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException(ex.Message);
        }
    }
}