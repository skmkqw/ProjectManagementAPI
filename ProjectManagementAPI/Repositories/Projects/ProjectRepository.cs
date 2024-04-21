using System.Xml.XPath;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.Entities;
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
        var projectEntities = await _context.Projects.AsNoTracking().ToListAsync();
        var projects = projectEntities.Select(x => Project.Create(x.Id, x.Name, x.Description));
        return projects;
    }

    public async Task<Project?> GetById(Guid id)
    {
        var projectEntity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (projectEntity == null)
        {
            return null;
        }

        return Project.Create(projectEntity.Id, projectEntity.Name, projectEntity.Description);
    }

    public async Task<ProjectEntity?> Create(ProjectEntity projectEntity)
    {
        await _context.Projects.AddAsync(projectEntity);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<ProjectEntity?> Update(Guid id, ProjectEntity projectEntity)
    {
        var project = await GetById(id);
        
        if (project == null)
        {
            return null;
        }
        
        project.Name = projectEntity.Name;
        project.Description = projectEntity.Description;
        _context.Projects.Update(projectEntity);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<int> Delete(Guid id)
    {
        int isDeleted = await _context.Projects.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return isDeleted;
    }
}