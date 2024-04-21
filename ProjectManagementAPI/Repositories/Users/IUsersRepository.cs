using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Users;

public interface IUsersRepository
{
    public Task<IEnumerable<User>> GetAll();

    public Task<User?> GetById(Guid id);
    
    public Task<UserEntity?> Create(UserEntity userEntity);

    public Task<UserEntity?> Update(Guid id, UserEntity userEntity);

    public Task<int> Delete(Guid id);
}