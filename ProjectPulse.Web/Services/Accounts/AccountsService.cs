using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectPulse.Web.Services.Accounts;

public class AccountsService : IAccountsService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly HttpClient _httpClient;

    public AccountsService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
    {
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClient;
    }

    public async Task<string?> Login(LoginUserDto loginUser)
    {
        var response = await _httpClient.PostAsJsonAsync("api/accounts/login", loginUser);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(responseContent);
            var token = jsonDoc.RootElement.GetProperty("token").GetString();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };
            var identity = new ClaimsIdentity(claims, "Auth");
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync("Auth", principal);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("UserId", userId, cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserName", loginUser.UserName, cookieOptions);
            return null;
        }
        else
        {
            return await response.Content.ReadAsStringAsync();
        }
    }

    public async Task<string?> Register(AddUserDto registerUser)
    {
        var response = await _httpClient.PostAsJsonAsync("api/accounts/register", registerUser);
    

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(responseContent);
            var token = jsonDoc.RootElement.GetProperty("token").GetString();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, registerUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, userId),
            };
            var identity = new ClaimsIdentity(claims, "Auth");
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync("Auth", principal);

            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("UserId", userId, cookieOptions);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserName", registerUser.UserName, cookieOptions);
            return null;
        }
        else
        {
            return await response.Content.ReadAsStringAsync();
        }
    }

    public async Task Logout()
    {
        _httpContextAccessor.HttpContext!.Response.Cookies.Delete("UserId");
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserName");
        
        await _httpContextAccessor.HttpContext!.SignOutAsync("Auth").ConfigureAwait(false);
    }
}