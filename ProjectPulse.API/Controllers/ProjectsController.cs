using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Projects;
using ProjectPulse.DataAccess.DTOs.Projects;
using ProjectPulse.DataAccess.DTOs.Tasks;
using ProjectPulse.DataAccess.Mappers;

namespace ProjectPulse.API.Controllers;

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
        var project = await _projectsService.GetProjectById(id);
        if (project != null)
        {
            return Ok(project.ToProjectDto());
        }

        return NotFound("Project not found");
    }
    
    [Authorize]
    [HttpGet("{projectId}/tasks")]
    public async Task<IActionResult> GetTasks([FromRoute] Guid projectId, [FromQuery] Guid? userId = null)
    {
        if (userId.HasValue)
        {
            var userTasks = await _projectsService.GetUserTasks(projectId, userId.Value);
            if (userTasks != null)
            {
                return Ok(userTasks.Select(t => t.ToTaskDto()));
            }
            return BadRequest("Tasks for the specified user not found");
        }
        else
        {
            var projectTasks = await _projectsService.GetProjectTasks(projectId);
            if (projectTasks != null)
            {
                return Ok(projectTasks.Select(t => t.ToTaskDto()));
            }
            return BadRequest("Project not found");
        }
    }

    [HttpGet("{projectId}/users")]
    public async Task<IActionResult> GetUsers([FromRoute] Guid projectId)
    {
        var users = await _projectsService.GetProjectUsers(projectId);
        if (users != null)
        {
            return Ok(users);
        }
    
        return BadRequest("Project not found");
    }
    
    #endregion GET EDPOINTS
    

    #region POST ENDPOINTS

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProjectDto createProjectDto, [FromQuery] Guid creatorId)
    {
        var project = await _projectsService.CreateProject(createProjectDto, creatorId);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project.ToProjectDto());
    }

    [HttpPost("{projectId}/add_task")]
    public async Task<IActionResult> AddTask([FromRoute] Guid projectId, [FromBody] CreateTaskDto createTaskDto)
    {
        var createdTask = await _projectsService.AddTask(projectId, createTaskDto);
        
        if (createdTask != null)
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask.ToTaskDto());
        
        return BadRequest("Project not found");
    }
    
    [HttpPost("{projectId}/add_user")]
    public async Task<IActionResult> AddUser([FromRoute] Guid projectId, [FromQuery] Guid userId)
    {
        var (createdEntity, error) = await _projectsService.AddUserToProject(projectId, userId);
        if (createdEntity != null)
        {
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.UserId }, createdEntity.ToDto());
        }
    
        return BadRequest(error);
    }

    #endregion POST ENDPOINTS

    
    #region PUT ENDPOINTS
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectDto updateProjectDto)
    {
        var updatedProject = await _projectsService.UpdateProject(id, updateProjectDto);
        if (updatedProject != null)
        {
            return Ok(updatedProject.ToProjectDto());
        }
        return NotFound("Project not found");
    }
    
    #endregion PUT ENDPOINTS


    #region DELETE ENDPOINTS

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        bool isDeleted = await _projectsService.DeleteProject(id);
        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound("Project not found");
    }
    
    [HttpDelete("{projectId}/remove_user")]
    public async Task<IActionResult> RemoveUser([FromRoute] Guid projectId, [FromQuery] Guid userId)
    {
        (Guid? removedUserId, string? error) = await _projectsService.RemoveUserFromProject(projectId, userId);
        if (removedUserId != null)
        {
            return Ok(removedUserId);
        }
    
        return BadRequest(error);
    }

    #endregion DELETE ENDPOINTS
}