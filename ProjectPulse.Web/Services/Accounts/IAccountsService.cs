using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectPulse.Web.Services.Accounts;

public interface IAccountsService
{
    public Task<string?> Login(LoginUserDto loginUser);
    
    public Task<string?> Register(AddUserDto registerUser);
    
    public Task Logout();
}