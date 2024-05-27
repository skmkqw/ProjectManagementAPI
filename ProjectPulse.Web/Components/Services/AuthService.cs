using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectPulse.Web.Components.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService()
    {
        _httpClient = new HttpClient();
    }

    public async Task Login(LoginUserDto loginModel)
    {
        // var response = await _httpClient.PostAsJsonAsync("http://localhost:5108/api/accounts/login", loginModel);
        //
        // if (response.IsSuccessStatusCode)
        // {
        //     var responseContent = await response.Content.ReadAsStringAsync();
        //     var jsonDoc = JsonDocument.Parse(responseContent);
        //     var token = jsonDoc.RootElement.GetProperty("token").GetString();
        //
        //     var handler = new JwtSecurityTokenHandler();
        //     var jwtToken = handler.ReadJwtToken(token);
        //     var userId = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;
        //
        //     var claims = new[]
        //     {
        //         new Claim(ClaimTypes.Name, loginModel.UserName),
        //         new Claim(ClaimTypes.NameIdentifier, userId),
        //     };
        //     var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //     var principal = new ClaimsPrincipal(identity);
        //
        //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //
        //     var cookieOptions = new CookieOptions
        //     {
        //         Expires = DateTimeOffset.UtcNow.AddDays(1),
        //         HttpOnly = true,
        //         Secure = true,
        //         SameSite = SameSiteMode.Strict
        //     };
        //     Response.Cookies.Append("UserId", userId, cookieOptions);
        // }
        // else
        // {
        // }
    }

    public async Task<bool> Register(AddUserDto registerModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/account/register", registerModel);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}