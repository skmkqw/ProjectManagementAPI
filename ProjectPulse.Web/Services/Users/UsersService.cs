namespace ProjectPulse.Web.Services.Users;

public class UsersService : IUsersService
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public UsersService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetUserId()
    {
        if (_httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue("UserId", out var userId))
        {
            return userId;
        }

        return "User ID not found";
    }

    public string GetUserName()
    {
        if (_httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue("UserName", out var userName))
        {
            return userName;
        }

        return "User Name not found";
    }
}