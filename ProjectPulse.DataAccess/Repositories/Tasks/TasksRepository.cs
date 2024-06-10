using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using ProjectPulse.DataAccess.DTOs.Tasks;

namespace ProjectPulse.DataAccess.Repositories.Tasks;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _context;

    private readonly UserManager<AppUser> _userManager;
    
    public TasksRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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

    public async Task<ProjectTaskEntity> UpdatePriority(ProjectTaskEntity taskEntity)
    {
        _context.Entry(taskEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return taskEntity;
    }

    public async Task<(ProjectTaskEntity? taskEntity, string? error)> AssignUser(Guid taskId, Guid userId)
    {
        var taskEntity = await _context.ProjectTasks.FindAsync(taskId);
        if (taskEntity == null)
        {
            return (null, "Task not found");
        }
        
        if (taskEntity.Status == TaskStatuses.Done)
        {
            return (null, "Can't reassign user for completed task!");
        }
        
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
        if (userEntity == null)
        {
            return (null, "User not found");
        }
    
        bool userExistsInProject = await UserExistsInProject(userId, taskEntity);
        if (userExistsInProject == false)
        {
            return (null, "Can't assign task to user until user is added to project");
        }
    
        taskEntity.AssignedUserId = userId;
        taskEntity.LastUpdateTime = DateTime.UtcNow;
    
        await _context.SaveChangesAsync();
    
        return (taskEntity, null);
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
    
    public async Task<string?> RemoveUser(Guid taskId)
    {
        var projectTaskEntity = await _context.ProjectTasks.FindAsync(taskId);
        if (projectTaskEntity == null)
        {
            return "Task not found";
        }

        if (projectTaskEntity.Status == TaskStatuses.Done)
        {
            return "Can't remove assigned user from completed task!";
        }

        projectTaskEntity.AssignedUser = null;
        projectTaskEntity.AssignedUserId = null;
        projectTaskEntity.LastUpdateTime = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return null;
    }

    #endregion DELETE METHODS

    private async Task<bool> UserExistsInProject(Guid userId, ProjectTaskEntity taskEntity)
    {
        var projectUsersEnity =
           await _context.ProjectUsers.FirstOrDefaultAsync(i => i.ProjectId == taskEntity!.ProjectId && i.UserId == userId);
        return projectUsersEnity != null;
    }
}