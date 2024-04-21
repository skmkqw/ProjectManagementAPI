using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Tasks;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Tasks;

public class TasksRepository : ITasksRepository
{
    private readonly ApplicationDbContext _context;

    public TasksRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<ProjectTask>> GetAll()
    {
        var taskEntities =  await _context.ProjectTasks.AsNoTracking().ToListAsync();
        var tasks = taskEntities.Select(x => ProjectTask.Create(x.Id, x.Title, x.Description, x.ProjectId));
        return tasks;
    }

    public async Task<ProjectTaskEntity?> GetById(Guid id)
    {
        var taskEntity = await _context.ProjectTasks.FirstOrDefaultAsync(x => x.Id == id);
        if (taskEntity == null)
        {
            return null;
        }

        ProjectTask.Create(taskEntity.Id, taskEntity.Title, taskEntity.Description, taskEntity.ProjectId);
        
        return taskEntity;
    }

    public async Task<IEnumerable<ProjectTask>> GetByProjectId(Guid projectId)
    {
        var taskEntities = await _context.ProjectTasks.Where(task => task.ProjectId == projectId).ToListAsync();
        
        if (!taskEntities.Any())
        {
            return null;
        }
        
        var tasks = taskEntities.Select(x => ProjectTask.Create(x.Id, x.Title, x.Description, x.ProjectId));
        return tasks;
    }

    public async Task<ProjectTaskEntity?> Create(ProjectTaskFromRequestDto projectTaskFromRequest)
    {
        var projectTaskEntity = projectTaskFromRequest.ToProjectTaskEntity();
        await _context.ProjectTasks.AddAsync(projectTaskEntity);
        await _context.SaveChangesAsync();
        return projectTaskEntity;
    }

    public async Task<ProjectTaskEntity?> Update(Guid id, ProjectTaskFromRequestDto projectTaskFromRequest)
    {
        var task = await GetById(id);
        
        if (task == null)
        {
            return null;
        }
        
        task.Title = projectTaskFromRequest.Title;
        task.Description = projectTaskFromRequest.Description;
        _context.ProjectTasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }
    
    public async Task<int> Delete(Guid id)
    {
        int isDeleted = await _context.ProjectTasks.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return isDeleted;
    }
}