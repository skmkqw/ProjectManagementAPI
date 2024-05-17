using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Projects;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public class ProjectsRepository : IProjectsRepository
{
    private readonly ApplicationDbContext _context;
    
    private readonly UserManager<AppUser> _userManager;

    public ProjectsRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    #region GET METHODS 

    public async Task<IEnumerable<ProjectEntity>> GetAll()
    {
        return await _context.Projects.AsNoTracking().ToListAsync();
    }

    public async Task<ProjectEntity?> GetById(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }
    
    public async Task<IEnumerable<ProjectTaskEntity>?> GetTasks(Guid projectId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);

        if (projectEntity == null)
        {
            return null;
        }

        return await _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToListAsync();
    }

    public async Task<IEnumerable<ProjectTaskEntity>?> GetUserTasks(Guid userId, Guid projectId)
    {
        var projectEnitty = await _context.Projects.FindAsync(projectId);
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
        if (userEntity == null || projectEnitty == null)
        {
            return null!;
        }

        return await _context.ProjectTasks.Where(pui => pui.ProjectId == projectId && pui.AssignedUserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<AppUser>?> GetUsers(Guid projectId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);

        if (projectEntity == null) return null;
        
        return await _context.ProjectUsers.
            Where(pu => pu.ProjectId == projectId)
            .Select(u => u.User)
            .ToListAsync();
    }

    #endregion GET METHODS


    #region POST ENDPOINTS

    public async Task<ProjectEntity> Create(ProjectEntity projectEntity)
    {
        await _context.Projects.AddAsync(projectEntity);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<ProjectTaskEntity?> AddTask(Guid projectId, ProjectTaskEntity taskEntity)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);

        if (projectEntity == null)
        {
            return null;
        }
        taskEntity.ProjectId = projectId;
        projectEntity.LastUpdateTime = DateTime.UtcNow;

        _context.ProjectTasks.Add(taskEntity);
        await _context.SaveChangesAsync();

        return taskEntity;
    }
    
    public async Task<(ProjectUserEntity? userEntity, string? error)> AddUser(Guid projectId, Guid userId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);
        if (projectEntity == null)
        {
            return (null, "Project not found");
        }
        
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
        if (userEntity == null)
        {
            return (null, "User not found");
        }
        
        var existingProjectUser = await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
        if (existingProjectUser != null)
        {
            return (null, "User already exists in the project!");
        }
    
        var projectUserEntity = new ProjectUserEntity()
        {
            ProjectId = projectId,
            Project = projectEntity,
            UserId = userId,
            User = userEntity
        };
    
        await _context.ProjectUsers.AddAsync(projectUserEntity);
        projectEntity.ProjectUsers.Add(projectUserEntity);
        projectEntity.LastUpdateTime = DateTime.UtcNow;
        userEntity.ProjectUsers.Add(projectUserEntity);
        
        await _context.SaveChangesAsync();
        
        return (projectUserEntity, null);
    }

    #endregion POST METHODS


    #region PUT METHODS

    public async Task<ProjectEntity> Update(ProjectEntity projectEntity, UpdateProjectDto updateProjectDto)
    {
        _context.Entry(projectEntity).CurrentValues.SetValues(updateProjectDto);
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task<bool> Delete(Guid id)
    {
        var projectEntity = await _context.Projects.FindAsync(id);
        if (projectEntity == null)
        {
            return false;
        }
        _context.Projects.Remove(projectEntity);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<(Guid? userId, string? error)> RemoveUser(Guid projectId, Guid userId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);
        if (projectEntity == null)
        {
            return (null, "Project not found");
        }
        
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
        if (userEntity == null)
        {
            return (null, "User not found");
        }
        
        var existingProjectUser = await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
        
        if (existingProjectUser == null)
        {
            return (null, "There is no such user in the project!");
        }
    
        _context.ProjectUsers.Remove(existingProjectUser);
        await _context.SaveChangesAsync();
        return (userId, null);
    }

    #endregion DELETE METHODS
}