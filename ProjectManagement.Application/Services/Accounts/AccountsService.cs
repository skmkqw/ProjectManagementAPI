using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    
    public async Task<(string? token, ModelStateDictionary modelStateErrors)> RegisterUser(AddUserDto registerDto, ModelStateDictionary modelState)
    {
        return await _repository.Register(registerDto, modelState);
    }

    public async Task<(string? token, string? error)> LoginUser(LoginUserDto loginDto)
    {
        return await _repository.Login(loginDto);
    }
}