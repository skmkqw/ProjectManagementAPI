using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.DataAccess.Repositories.Accounts;

public class AccountsRepository : IAccountsRepository
{
    private readonly UserManager<AppUser> _userManager;

    private readonly IConfiguration _configuration;

    public AccountsRepository(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<(string? token, string? error)> Register(AddUserDto registerDto)
    {
        
        var exisingUser = await _userManager.FindByNameAsync(registerDto.UserName);

        if (exisingUser != null)
        {
            return (null, "Username is already taken!");
        }

        var user = new AppUser()
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            var token = GenerateToken(registerDto.UserName);
            return (token, null);
        }

        return (null, "Failed to create user");
    }

    public async Task<(string? token, string? error)> Login(LoginUserDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName); 
        if (user != null) 
        { 
            if (await _userManager.CheckPasswordAsync(user, loginDto.Password)) 
            { 
                var token = GenerateToken(loginDto.UserName);
                return (token, null);
            } 
        } 
        
        return (null, "Invalid username or password");
    }

    private string? GenerateToken(string userName)
    {
        var secret = _configuration["JwtConfig:Secret"];
        var issuer = _configuration["JwtConfig:ValidIssuer"];
        var audience = _configuration["JwtConfig:ValidAudiences"];

        if (secret == null || issuer == null || audience == null)
        {
            throw new ApplicationException("Jwt is not set in configuration");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, userName)}),
            Expires = DateTime.UtcNow.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);
        return token;
    }
}