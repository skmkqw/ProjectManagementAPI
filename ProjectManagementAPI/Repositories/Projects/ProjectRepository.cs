using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Project;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Projects;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Project>> GetAll()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project?> GetById(Guid id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (project == null)
        {
            return null;
        }

        return project;
    }

    public async Task<Project?> Create(ProjectFromRequestDto projectFromRequestDto)
    {
        var project = projectFromRequestDto.ToProjectFromRequestDto();
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> Update(Guid id, ProjectFromRequestDto projectFromRequestDto)
    {
        var project = await GetById(id);
        
        if (project == null)
        {
            return null;
        }
        
        project.Name = projectFromRequestDto.Name;
        project.Description = projectFromRequestDto.Description;
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> Delete(Guid id)
    { 
        var project = await GetById(id);
        
        if (project == null)
        {
            return null;
        }
        
        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return project;
    }
}