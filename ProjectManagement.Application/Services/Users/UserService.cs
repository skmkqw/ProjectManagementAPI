using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;
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
    
    #region GET METHODS

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var userEntities = await _usersRepository.GetAll();
        return userEntities.Select(u => u.ToUserModel());
    }

    public async Task<User> GetUserById(Guid id)
    {
        var userEntity = await _usersRepository.GetById(id);
        
        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found!");
        }

        return userEntity.ToUserModel();
    }
    
    public async Task<IEnumerable<ProjectTask>> GetUserTasks(Guid userId)
    {
        try
        {
            var taskEntities = await _usersRepository.GetTasks(userId);
            return taskEntities.Select(t => t.ToTaskModel());
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    public async Task<IEnumerable<Project>> GetUserProjects(Guid userId)
    {
        try
        {
            var projectEntities = await _usersRepository.GetProjects(userId);
            return projectEntities.Select(p => p.ToProjectModel());
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
    }

    #endregion GET METHODS


    #region POST METHODS

    public async Task<User> CreateUser(CreateUserDto createUser)
    {
        var userEntity = createUser.FromCreateDtoToUserEntity();

        var createdEntity = await _usersRepository.Create(userEntity);

        return createdEntity.ToUserModel();
    }

    #endregion POST METHODS


    #region PUT METHODS

    public async Task<User> UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var userEntity = await _usersRepository.GetById(id);

        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        await _usersRepository.Update(userEntity, updateUserDto);

        return userEntity.ToUserModel();
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task<bool> DeleteUser(Guid id)
    {
        bool isDeleted = await _usersRepository.Delete(id);
        return isDeleted;
    }

    #endregion DELETE METHODS
}