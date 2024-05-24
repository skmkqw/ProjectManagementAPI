using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectPulse.DataAccess.Repositories.Accounts;

public interface IAccountsRepository
{
    public Task<(string? token, ModelStateDictionary modelStateErrors)> Register(AddUserDto registerDto, ModelStateDictionary modelState);
    
    public Task<(string? token, string? error)> Login(LoginUserDto loginDto);
}