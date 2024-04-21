using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Repositories.Tasks;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _repository;

    public TasksController(ITasksRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _repository.GetAll();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var task = await _repository.GetById(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpGet("project_id/{projectId}")]
    public async Task<IActionResult> GetByProjectId([FromRoute] Guid projectId)
    {
        var tasks = await _repository.GetByProjectId(projectId);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectTaskEntity projectTaskEntity)
    {
        var task = await _repository.Create(projectTaskEntity);
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectTaskEntity projectTaskEntity)
    {
        var task = await _repository.Update(id, projectTaskEntity);

        if (task == null)
        {
            return NotFound();
        }
        
        return Ok(task);
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