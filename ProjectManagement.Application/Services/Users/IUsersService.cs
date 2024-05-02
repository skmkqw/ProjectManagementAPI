using System.Collections;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Users;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();

    public Task<User> GetUserById(Guid id);

    public Task<IEnumerable<ProjectTask>> GetUserTasks(Guid userId);

    public Task<IEnumerable<Project>> GetUserProjects(Guid userId);
    
    public Task<User> CreateUser(CreateUserDto createUser);
    
    public Task<User> UpdateUser(Guid id, UpdateUserDto updateUserDto);
    
    public Task<bool> DeleteUser(Guid id);
}