using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Users;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Mappers;

namespace ProjectManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    #region GET ENDPOINTS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _usersService.GetAllUsers();

        return Ok(users.Select(u => u.FromUserModelToDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var user = await _usersService.GetUserById(id);
            return Ok(user.FromUserModelToDto());
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{userId}/tasks")]
    public async Task<IActionResult> GetTasks([FromRoute] Guid userId)
    {
        try
        {
            var tasks = await _usersService.GetUserTasks(userId);
            return Ok(tasks.Select(t => t.ToTaskDto()));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{userId}/projects")]
    public async Task<IActionResult> GetProjects([FromRoute] Guid userId)
    {
        try
        {
            var projects = await _usersService.GetUserProjects(userId);
            return Ok(projects.Select(p => p.ToProjectDto()));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    #endregion GET ENDPOINTS


    #region POST ENDPOINTS

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUser)
    {
        var user = await _usersService.CreateUser(createUser);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.FromUserModelToDto());
    }

    #endregion POST ENDPOINTS


    #region PUT ENDPOINTS

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            var updatedUser = await _usersService.UpdateUser(id, updateUserDto);
            return Ok(updatedUser.FromUserModelToDto());
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    #endregion PUT ENDPOINTS


    #region DELETE ENDPOINTS

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool isDeleted = await _usersService.DeleteUser(id);
        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound("User not found");
    }

    #endregion DELETE ENDPOINTS
}