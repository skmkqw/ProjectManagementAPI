using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Project> GetAll()
    {
        return _context.Projects.ToList();
    }

    public Project? GetById(int id)
    {
        var project = _context.Projects.FirstOrDefault(x => x.Id == id);
        if (project == null)
        {
            return null;
        }

        return project;
    }

    public Project Create(ProjectFromRequestDto projectFromRequestDto)
    {
        var project = projectFromRequestDto.ToProjectFromRequestDto();
        _context.Projects.Add(project);
        _context.SaveChanges();
        return project;
    }

    public Project? Update(int id, ProjectFromRequestDto projectFromRequestDto)
    {
        var project = GetById(id);
        
        if (project == null)
        {
            return null;
        }
        
        project.Name = projectFromRequestDto.Name;
        project.Description = projectFromRequestDto.Description;
        _context.Projects.Update(project);
        _context.SaveChanges();
        return project;
    }

    public Project? Delete(int id)
    {
        var project = GetById(id);
        
        if (project == null)
        {
            return null;
        }
        
        _context.Projects.Remove(project);

        _context.SaveChanges();

        return project;
    }
}