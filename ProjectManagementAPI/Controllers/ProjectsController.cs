using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var projects = _dbContext.Projects.ToList().Select(x => x.ToProjectDto());

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        return Ok(project.ToProjectDto()); 
    }

    [HttpPost]
    public IActionResult Create([FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = projectFromRequestDto.ToProjectFromRequestDto();
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] ProjectFromRequestDto projectFromRequestDto)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        project.Name = projectFromRequestDto.Name;
        project.Description = projectFromRequestDto.Description;
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        _dbContext.Projects.Remove(project);

        _dbContext.SaveChanges();

        return NoContent();
    }
    
}