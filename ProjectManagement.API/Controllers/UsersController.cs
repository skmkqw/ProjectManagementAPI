using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Users;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Mappers;
using ProjectManagement.DataAccess.Repositories.Users;

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
        var user = await _usersService.GetUserById(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.FromUserModelToDto()); 
    }
    
    [HttpGet("{userId}/tasks")]
    public async Task<IActionResult> GetTasks([FromRoute] Guid userId)
    {
        try
        {
            var tasks = await _usersService.GetTasks(userId);
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
            var projects = await _usersService.GetProjects(userId);
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
    public async Task<IActionResult> Create([FromBody] UserFromRequestDto userFromRequest)
    {
        var user = await _usersService.CreateUser(userFromRequest);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.FromUserModelToDto());
    }

    #endregion POST ENDPOINTS


    #region PUT ENDPOINTS

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserFromRequestDto userFromRequest)
    {
        try
        {
            await _usersService.UpdateUser(id, userFromRequest);
            return NoContent();
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
        try
        {
            await _usersService.DeleteUser(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    #endregion DELETE ENDPOINTS
}