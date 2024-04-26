using System.Collections;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Users;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();

    public Task<User> GetUserById(Guid id);

    public Task<IEnumerable<ProjectTask>> GetTasks(Guid userId);

    public Task<IEnumerable<Project>> GetProjects(Guid userId);
    
    public Task<User> CreateUser(UserFromRequestDto userFromRequest);
    
    public Task<User> UpdateUser(Guid id, UserFromRequestDto userFromRequest);
    
    public Task DeleteUser(Guid id);
}