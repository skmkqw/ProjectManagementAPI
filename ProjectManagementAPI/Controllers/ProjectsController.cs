using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;
using ProjectManagementAPI.Repositories;

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
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var project = await _projectRepository.GetById(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.ToProjectDto()); 
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = await _projectRepository.Create(projectFromRequestDto);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = await _projectRepository.Update(id, projectFromRequestDto);
        
        if (project == null)
        {
            return NotFound();
        }
        
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var project = await _projectRepository.Delete(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}