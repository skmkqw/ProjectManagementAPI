using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Users;

public interface IUsersRepository
{
    public Task<IEnumerable<User>> GetAll();

    public Task<UserEntity?> GetById(Guid id);
    
    public Task<UserEntity?> Create(UserFromRequestDto userFromRequest);

    public Task<UserEntity?> Update(Guid id, UserFromRequestDto userFromRequest);

    public Task<int> Delete(Guid id);
}