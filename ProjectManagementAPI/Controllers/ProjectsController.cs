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
    public IActionResult GetAll()
    {
        var projects = _projectRepository.GetAll();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var project = _projectRepository.GetById(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.ToProjectDto()); 
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = _projectRepository.Create(projectFromRequestDto);
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = _projectRepository.Update(id, projectFromRequestDto);
        
        if (project == null)
        {
            return NotFound();
        }
        
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var project = _projectRepository.Delete(id);
        
        if (project == null)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}