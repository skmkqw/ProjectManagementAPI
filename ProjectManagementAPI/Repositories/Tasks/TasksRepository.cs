using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.ProjectTask;
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
        return await _context.ProjectTasks.ToListAsync();
    }

    public async Task<ProjectTask?> GetById(int id)
    {
        var task = await _context.ProjectTasks.FirstOrDefaultAsync(x => x.Id == id);
        if (task == null)
        {
            return null;
        }

        return task;
    }

    public async Task<IEnumerable<ProjectTask>> GetByProjectId(int projectId)
    {
        var tasks = await _context.ProjectTasks.Where(task => task.ProjectId == projectId).ToListAsync();
        
        if (!tasks.Any())
        {
            return null;
        }

        return tasks;
    }

    public async Task<ProjectTask?> Create(TaskFromRequestDto taskFromRequestDto)
    {
        var task = taskFromRequestDto.ToTaskFromRequestDto();
        await _context.ProjectTasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<ProjectTask?> Update(int id, TaskFromRequestDto taskFromRequestDto)
    {
        var task = await GetById(id);
        
        if (task == null)
        {
            return null;
        }
        
        task.Title = taskFromRequestDto.Title;
        task.Description = taskFromRequestDto.Description;
        _context.ProjectTasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }
    
    public async Task<ProjectTask?> Delete(int id)
    {
        var task = await GetById(id);
        
        if (task == null)
        {
            return null;
        }
        
        _context.ProjectTasks.Remove(task);

        await _context.SaveChangesAsync();

        return task;
    }
}