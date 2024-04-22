using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Users;

public interface IUsersService
{
    public Task<IEnumerable<User>> GetAllUsers();

    public Task<User> GetUserById(Guid id);

    public Task<User> CreateUser(UserFromRequestDto userFromRequest);
    
    public Task<User> UpdateUser(Guid id, UserFromRequestDto userFromRequest);
    
    public Task DeleteUser(Guid id);
}