using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Tasks;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksService _tasksService;

    public TasksController(ITasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _tasksService.GetAllTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var task = await _tasksService.GetTasktById(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpGet("project_id/{projectId}")]
    public async Task<IActionResult> GetByProjectId([FromRoute] Guid projectId)
    {
        var tasks = await _tasksService.GetTasktByProjectId(projectId);

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectTaskFromRequestDto projectTaskFromRequest)
    {
        var task = await _tasksService.CreateTask(projectTaskFromRequest);
        
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectTaskFromRequestDto projectTaskFromRequest)
    {
        try
        {
            await _tasksService.UpdateTask(id, projectTaskFromRequest);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _tasksService.DeleteTask(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}