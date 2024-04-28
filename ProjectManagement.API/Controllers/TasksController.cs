using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Tasks;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Mappers;

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

    #region GET ENDPOINTS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _tasksService.GetAllTasks();
        return Ok(tasks.Select(t => t.FromTaskModelToDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var task = await _tasksService.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task.FromTaskModelToDto());
    }

    #endregion GET ENDPOINTS
    
    
    #region PUT ENDPOINTS

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            await _tasksService.UpdateTask(id, updateTaskDto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromQuery] TaskStatuses status)
    {
        try
        {
            await _tasksService.UpdateTaskStatus(id, status);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{taskId}/assign_user")]
    public async Task<IActionResult> AssignUser([FromRoute] Guid taskId, [FromQuery] Guid userId)
    {
        try
        {
            Guid assignedUserId = await _tasksService.AssignUserToTask(taskId, userId);
            return Ok(assignedUserId);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
    }

    #endregion PUT ENDPOINTS
    
    
    #region DELETE ENDPOINTS

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

    [HttpDelete("{taskId}/remove_user")]
    public async Task<IActionResult> RemoveUser([FromRoute] Guid taskId)
    {
        try
        {
            await _tasksService.RemoveUserFromTask(taskId);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    #endregion DELETE ENDPOINTS
}