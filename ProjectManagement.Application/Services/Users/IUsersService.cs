using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Users;

public interface IUsersService
{
    public Task<IEnumerable<AppUser>> GetAllUsers();

    public Task<AppUser?> GetUserById(Guid id);
    
    public Task<IEnumerable<ProjectTask>?> GetUserTasks(Guid userId);
    
    public Task<IEnumerable<Project>?> GetUserProjects(Guid userId);
    
    public Task<AppUser?> UpdateUser(Guid id, UpdateUserDto updateUserDto);
    
    public Task<bool> DeleteUser(Guid id);
}