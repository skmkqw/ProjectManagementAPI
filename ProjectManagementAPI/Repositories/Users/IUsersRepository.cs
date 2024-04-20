using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Users;

public interface IUsersRepository : IGenericRepository<User>
{
    public Task<User?> Create(UserFromRequestDto userFromRequestDto);

    public Task<User?> Update(Guid id, UserFromRequestDto userFromRequestDto);
}