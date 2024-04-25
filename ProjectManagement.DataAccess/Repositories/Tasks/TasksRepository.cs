using Microsoft.EntityFrameworkCore;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Tasks;
using ProjectManagement.DataAccess.Entities;

namespace ProjectManagement.DataAccess.Repositories.Tasks;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _context;

    public TasksRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectTaskEntity>> GetAll()
    {
        return await _context.ProjectTasks.AsNoTracking().ToListAsync();
    }

    public async Task<ProjectTaskEntity?> GetById(Guid id)
    {
        return await _context.ProjectTasks.FindAsync(id);
    }

    public async Task<Guid> AssignUser(Guid taskId, Guid userId)
    {
        var taskEntity = await _context.ProjectTasks.FindAsync(taskId);
        if (taskEntity == null)
        {
            throw new KeyNotFoundException("Task not found!");
        }
        
        var userEntity = await _context.Users.FindAsync(userId);
        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found!");
        }

        taskEntity.AssignedUserId = userId;

        await _context.SaveChangesAsync();

        return userId;
    }

    public async Task<ProjectTaskEntity?> Update(ProjectTaskEntity projectTaskEntity)
    {
        _context.Entry(projectTaskEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return projectTaskEntity;
    }

    public async Task Delete(Guid id)
    {
        var projectTaskEntity = await _context.ProjectTasks.FindAsync(id);
        if (projectTaskEntity == null)
        {
            throw new KeyNotFoundException("Task not found!");
        }
        _context.ProjectTasks.Remove(projectTaskEntity);
        await _context.SaveChangesAsync();
    }
}