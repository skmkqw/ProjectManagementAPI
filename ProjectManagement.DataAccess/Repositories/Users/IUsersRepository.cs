using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.Repositories.Users;

public interface IUsersRepository
{
    public Task<IEnumerable<UserEntity>> GetAll();

    public Task<UserEntity?> GetById(Guid id);
    
    public Task<UserEntity?> Create(UserEntity userEntity);
    
    public Task<IEnumerable<ProjectTaskEntity>> GetTasks(Guid projectId);

    public Task<UserEntity?> Update(UserEntity userEntity);

    public Task Delete(Guid id);
}