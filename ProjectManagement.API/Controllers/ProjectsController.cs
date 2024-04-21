using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Projects;
using ProjectManagement.DataAccess.DTOs.Projects;

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

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var project = await _projectsService.GetProjectById(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = await _projectsService.CreateProject(projectFromRequestDto);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
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