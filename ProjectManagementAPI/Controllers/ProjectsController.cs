using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Repositories.Projects;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _projectRepository.GetAll();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var project = await _projectRepository.GetById(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectEntity projectEntity)
    {
        var project = await _projectRepository.Create(projectEntity);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProjectEntity projectEntity)
    {
        var project = await _projectRepository.Update(id, projectEntity);
        
        if (project == null)
        {
            return NotFound();
        }
        
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var isDeleted = await _projectRepository.Delete(id);
        
        if (isDeleted == 0)
        {
            return NotFound();
        }

        return NoContent();
    }
}