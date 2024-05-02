using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Tasks;

namespace ProjectManagement.DataAccess.Repositories.Tasks;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _context;

    public TasksRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GET METHODS

    public async Task<IEnumerable<ProjectTaskEntity>> GetAll()
    {
        return await _context.ProjectTasks.AsNoTracking().ToListAsync();
    }

    public async Task<ProjectTaskEntity?> GetById(Guid id)
    {
        return await _context.ProjectTasks.FindAsync(id);
    }

    #endregion GET METHODS
    
    
    #region PUT METHODS

    public async Task<ProjectTaskEntity> Update(ProjectTaskEntity projectTaskEntity, UpdateTaskDto updateTaskDto)
    {
        _context.Entry(projectTaskEntity).CurrentValues.SetValues(updateTaskDto);
        await _context.SaveChangesAsync();
        return projectTaskEntity;
    }

    public async Task<ProjectTaskEntity> UpdateStatus(ProjectTaskEntity taskEntity)
    {
        _context.Entry(taskEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return taskEntity;
    }

    public async Task<ProjectTaskEntity> AssignUser(Guid taskId, Guid userId)
    {
        var taskEntity = await _context.ProjectTasks.FindAsync(taskId);
        if (taskEntity == null)
        {
            throw new KeyNotFoundException("Task not found!");
        }
        
        if (taskEntity.Status == TaskStatuses.Done)
        {
            throw new ArgumentException("Can't reassign user for completed task!");
        }
        
        var userEntity = await _context.Users.FindAsync(userId);
        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found!");
        }

        taskEntity.AssignedUserId = userId;
        taskEntity.LastUpdateTime = DateTime.Now;

        await _context.SaveChangesAsync();

        return taskEntity;
    }

    #endregion PUT METHODS
    
    
    #region DELETE METHODS

    public async Task<bool> Delete(Guid id)
    {
        var taskEntity = await _context.ProjectTasks.FindAsync(id);
        if (taskEntity == null)
        {
            return false;
        }
        _context.ProjectTasks.Remove(taskEntity);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task RemoveUser(Guid taskId)
    {
        var projectTaskEntity = await _context.ProjectTasks.FindAsync(taskId);
        if (projectTaskEntity == null)
        {
            throw new KeyNotFoundException("Task not found!");
        }

        if (projectTaskEntity.Status == TaskStatuses.Done)
        {
            throw new ArgumentException("Can't remove assigned user from completed task!");
        }

        projectTaskEntity.AssignedUser = null;
        projectTaskEntity.AssignedUserId = null;
        projectTaskEntity.LastUpdateTime = DateTime.Now;

        
        await _context.SaveChangesAsync();
    }

    #endregion DELETE METHODS
}