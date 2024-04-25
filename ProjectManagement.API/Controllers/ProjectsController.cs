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
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectsService.GetAllProjects();

        return Ok(projects.Select(p => p.FromProjectModelToDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var project = await _projectsService.GetProjectById(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.FromProjectModelToDto()); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = await _projectsService.CreateProject(projectFromRequestDto);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPost("{projectId}/add_task")]
    public async Task<IActionResult> AddTask([FromRoute] Guid projectId, [FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            var createdTask = await _projectsService.AddTask(projectId, createTaskDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask.FromTaskModelToDto());
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        try
        {
            await _projectsService.UpdateProject(id, projectFromRequestDto);
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
            await _projectsService.DeleteProject(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}