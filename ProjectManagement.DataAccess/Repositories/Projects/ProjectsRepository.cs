using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public class ProjectsRepository : IProjectsRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectsRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProjectEntity>> GetAll()
    {
        return await _context.Projects.AsNoTracking().Include(p => p.Tasks).ToListAsync();
    }

    public async Task<ProjectEntity?> GetById(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<ProjectEntity?> Create(ProjectEntity projectEntity)
    {
        await _context.Projects.AddAsync(projectEntity);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<ProjectTaskEntity> AddTask(Guid projectId, ProjectTaskEntity taskEntity)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);
        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }

        taskEntity.ProjectId = projectId;

        _context.ProjectTasks.Add(taskEntity);
        await _context.SaveChangesAsync();

        return taskEntity;
    }

    public async Task<ProjectEntity?> Update(ProjectEntity projectEntity)
    {
        _context.Entry(projectEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task Delete(Guid id)
    {
        var projectEntity = await _context.Projects.FindAsync(id);
        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }
        _context.Projects.Remove(projectEntity);
        await _context.SaveChangesAsync();
    }
}