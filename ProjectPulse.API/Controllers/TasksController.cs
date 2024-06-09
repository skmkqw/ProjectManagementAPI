using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Tasks;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.DTOs.Tasks;
using ProjectPulse.DataAccess.Mappers;

namespace ProjectPulse.API.Controllers;

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
        return Ok(tasks.Select(t => t.ToTaskDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var task = await _tasksService.GetTaskById(id);
        if (task != null)
        {
            return Ok(task.ToTaskDto());
        }

        return NotFound("Task not found");
    }

    #endregion GET ENDPOINTS
    
    
    #region PUT ENDPOINTS

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var updatedTask = await _tasksService.UpdateTask(id, updateTaskDto);
        if (updatedTask != null)
        {
            return Ok(updatedTask.ToTaskDto());
        }

        return NotFound("Task not found");
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromQuery] TaskStatuses status)
    {
        (var updatedTask, string? error) = await _tasksService.UpdateTaskStatus(id, status);
        if (updatedTask != null)
        {
            return Ok(updatedTask.ToTaskDto());
        }

        return BadRequest(error);
    }

    [HttpPut("{taskId}/assign_user")]
    public async Task<IActionResult> AssignUser([FromRoute] Guid taskId, [FromQuery] Guid userId)
    {
        (var task, string? error) = await _tasksService.AssignUserToTask(taskId, userId);
        if (task != null)
        {
            return Ok(task.ToTaskDto());
        }
    
        return BadRequest(error);
    }

    #endregion PUT ENDPOINTS
    
    
    #region DELETE ENDPOINTS

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool isDeleted = await _tasksService.DeleteTask(id);
        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound("Task not found");
    }

    [HttpDelete("{taskId}/remove_user")]
    public async Task<IActionResult> RemoveUser([FromRoute] Guid taskId)
    {
        string? error = await _tasksService.RemoveUserFromTask(taskId);
        return error != null ? BadRequest(error) : NoContent();
    }

    #endregion DELETE ENDPOINTS
}