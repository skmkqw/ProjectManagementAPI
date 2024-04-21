using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Entities;
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
        var users = await _repository.GetAll();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var user = await _repository.GetById(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserFromRequestDto userFromRequest)
    {
        var user = await _repository.Create(userFromRequest);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserFromRequestDto userFromRequest)
    {
        var user = await _repository.Update(id, userFromRequest);
        
        if (user == null)
        {
            return NotFound();
        }
        
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var isDeleted = await _repository.Delete(id);
        
        if (isDeleted == 0)
        {
            return NotFound();
        }

        return NoContent();
    }
}