using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Repositories.Accounts;

namespace ProjectManagement.Application.Services.Accounts;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _repository;

    public AccountsService(IAccountsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<(string? token, string? error)> RegisterUser(AddUserDto registerDto)
    {
        return await _repository.Register(registerDto);
    }
}