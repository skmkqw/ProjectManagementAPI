using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Projects;
using ProjectManagement.DataAccess.Entities;
using ProjectManagement.DataAccess.Mappers;

namespace ProjectManagement.DataAccess.Repositories.Projects;

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

    public async Task<ProjectEntity?> GetById(Guid id)
    {
        var projectEntity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (projectEntity == null)
        {
            return null;
        }

        Project.Create(projectEntity.Id, projectEntity.Name, projectEntity.Description);
            
        return projectEntity;
    }

    public async Task<ProjectEntity?> Create(ProjectFromRequestDto projectFromRequestDto)
    {
        var projectEntity = projectFromRequestDto.ToProjectEntity();
        await _context.Projects.AddAsync(projectEntity);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<ProjectEntity?> Update(Guid id, ProjectFromRequestDto projectFromRequestDto)
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

    public async Task<int> Delete(Guid id)
    {
        int isDeleted = await _context.Projects.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return isDeleted;
    }
}