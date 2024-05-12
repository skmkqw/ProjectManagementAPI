using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Application.Services.Accounts;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Users;

namespace ProjectManagement.API.Controllers;

[ApiController]
[Route("/api/accounts")]
public class AccountController(UserManager<AppUser> userManager, IConfiguration configuration, IAccountsService accountsService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AddUserDto registerDto)
    {
        if (ModelState.IsValid)
        {
            (string? token, string? error) = await accountsService.RegisterUser(registerDto);
            if (token != null)
            {
                return Ok(new { token });
            }

            return BadRequest(error);
        }

        return BadRequest();
    }
}