using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.Repositories.Accounts;

public interface IAccountsRepository
{
    public Task<(string? token, string? error)> Register(AddUserDto registerDto);
}