using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Mappers;

namespace ProjectManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    #region GET ENDPOINTS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectsService.GetAllProjects();

        return Ok(projects.Select(p => p.ToProjectDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var project = await _projectsService.GetProjectById(id);
            return Ok(project.ToProjectDto()); 
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet("{projectId}/tasks")]
    public async Task<IActionResult> GetTasks([FromRoute] Guid projectId)
    {
        try
        {
            var tasks = await _projectsService.GetProjectTasks(projectId);
            return Ok(tasks.Select(t => t.ToTaskDto()));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("{projectId}/users")]
    public async Task<IActionResult> GetUsers([FromRoute] Guid projectId)
    {
        try
        {
            var users = await _projectsService.GetProjectUsers(projectId);
            return Ok(users.Select(u => u.FromUserModelToDto()));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    #endregion GET EDPOINTS
    

    #region POST ENDPOINTS

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto createProjectDto)
    {
        var project = await _projectsService.CreateProject(createProjectDto);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project.ToProjectDto());
    }

    [HttpPost("{projectId}/add_task")]
    public async Task<IActionResult> AddTask([FromRoute] Guid projectId, [FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            var createdTask = await _projectsService.AddTask(projectId, createTaskDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask.ToTaskDto());
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("{projectId}/add_user")]
    public async Task<IActionResult> AddUser([FromRoute] Guid projectId, [FromQuery] Guid userId)
    {
        try
        {
            var createdEntity = await _projectsService.AddUserToProject(projectId, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.UserId }, createdEntity.ToDto());
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    #endregion POST ENDPOINTS

    
    #region PUT ENDPOINTS
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectDto updateProjectDto)
    {
        try
        {
            var updatedProjectEntity = await _projectsService.UpdateProject(id, updateProjectDto);
            return Ok(updatedProjectEntity);
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
            await _projectsService.DeleteProject(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{projectId}/remove_user")]
    public async Task<IActionResult> RemoveUser([FromRoute] Guid projectId, [FromQuery] Guid userId)
    {
        try
        {
            await _projectsService.RemoveUserFromProject(projectId, userId);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    #endregion DELETE ENDPOINTS
}