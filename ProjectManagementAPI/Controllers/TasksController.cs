using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.DTOs.ProjectTask;
using ProjectManagementAPI.Mappers;
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
        var tasksDtos = tasks.Select(x => x.ToTaskDto());
        return Ok(tasksDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var task = await _repository.GetById(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task.ToTaskDto());
    }

    [HttpGet("project_id/{projectId}")]
    public async Task<IActionResult> GetByProjectId([FromRoute] Guid projectId)
    {
        var tasks = await _repository.GetByProjectId(projectId);

        if (tasks == null)
        {
            return NotFound();
        }

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskFromRequestDto requestDto)
    {
        var task = await _repository.Create(requestDto);
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TaskFromRequestDto requestDto)
    {
        var task = await _repository.Update(id, requestDto);

        if (task == null)
        {
            return NotFound();
        }
        
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var task = await _repository.Delete(id);

        if (task == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}