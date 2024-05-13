using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            (string? token, ModelStateDictionary modelStateErrors) = await accountsService.RegisterUser(registerDto, ModelState);
            if (token != null)
            {
                return Ok(new { token });
            }

            return BadRequest(modelStateErrors);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
    {
        if (ModelState.IsValid)
        {
            (string? token, string? error) = await accountsService.LoginUser(loginDto);
            if (token != null)
            {
                return Ok(new { token });
            }

            return BadRequest(error);
        }

        return BadRequest();
    }
}