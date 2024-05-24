using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectManagement.Application.Services.Accounts;

public interface IAccountsService
{
    public Task<(string? token, ModelStateDictionary modelStateErrors)> RegisterUser(AddUserDto registerDto, ModelStateDictionary modelState);
    
    public Task<(string? token, string? error)> LoginUser(LoginUserDto loginDto);
}