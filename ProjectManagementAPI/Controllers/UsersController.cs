using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Repositories.Users;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;

    public UsersController(IUsersRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _repository.GetAll();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await _repository.GetById(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.ToUserDto()); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserFromRequestDto userFromRequestDto)
    {
        var user = await _repository.Create(userFromRequestDto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UserFromRequestDto userFromRequestDto)
    {
        var user = await _repository.Update(id, userFromRequestDto);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var user = await _repository.Delete(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}