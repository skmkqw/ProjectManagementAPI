using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.Repositories.Users;

public interface IUsersRepository
{
    public Task<IEnumerable<AppUser>> GetAll();

    public Task<AppUser?> GetById(Guid id);
    
    public Task<IEnumerable<ProjectTaskEntity>?> GetTasks(Guid userId);
    
    public Task<IEnumerable<ProjectEntity>?> GetProjects(Guid userId);
    
    public Task<AppUser> Update(AppUser userEntity, UpdateUserDto updateUserDto);

    public Task<bool> Delete(Guid id);
}