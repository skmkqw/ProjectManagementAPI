using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Accounts;

public interface IAccountsService
{
    public Task<(string? token, string? error)> RegisterUser(AddUserDto registerDto);
}