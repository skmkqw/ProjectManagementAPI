using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;

namespace ProjectManagement.DataAccess.Repositories.Projects;

public class ProjectsRepository : IProjectsRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GET METHODS 

    public async Task<IEnumerable<ProjectEntity>> GetAll()
    {
        return await _context.Projects.AsNoTracking()
            .Include(p => p.Tasks)
            .Include(u => u.ProjectUsers)
            .ThenInclude(u => u.User)
            .ThenInclude(t => t.Tasks)
            .ToListAsync();
    }

    public async Task<ProjectEntity?> GetById(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(u => u.ProjectUsers)
            .ThenInclude(u => u.User)
            .ThenInclude(t => t.Tasks)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<IEnumerable<ProjectTaskEntity>> GetTasks(Guid projectId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);

        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project doesn't exist");
        }

        return await _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToListAsync();
    }
    
    public async Task<IEnumerable<UserEntity>> GetUsers(Guid projectId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);

        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project doesn't exist");
        }

        return await _context.ProjectUsers.
            Where(pu => pu.ProjectId == projectId)
            .Include(t => t.User.Tasks)
            .Select(u => u.User)
            .ToListAsync();
    }

    #endregion GET METHODS


    #region POST ENDPOINTS

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

    #endregion POST METHODS


    #region PUT METHODS

    public async Task<ProjectEntity?> Update(ProjectEntity projectEntity)
    {
        _context.Entry(projectEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return projectEntity;
    }

    public async Task<ProjectUserEntity> AddUser(Guid projectId, Guid userId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);
        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }
        
        var userEntity = await _context.Users.FindAsync(userId);
        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found!");
        }

        var projectUserEntity = new ProjectUserEntity()
        {
            ProjectId = projectId,
            Project = projectEntity,
            UserId = userId,
            User = userEntity
        };
        
        var existingProjectUser = await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);

        if (existingProjectUser != null)
        {
            throw new ArgumentException("User already exists in the project!");
        }

        await _context.ProjectUsers.AddAsync(projectUserEntity);
        await _context.SaveChangesAsync();
        
        projectEntity.ProjectUsers.Add(projectUserEntity);
        userEntity.ProjectUsers.Add(projectUserEntity);
        
        return projectUserEntity;
    }
    
    public async Task RemoveUser(Guid projectId, Guid userId)
    {
        var projectEntity = await _context.Projects.FindAsync(projectId);
        if (projectEntity == null)
        {
            throw new KeyNotFoundException("Project not found!");
        }
        
        var userEntity = await _context.Users.FindAsync(userId);
        if (userEntity == null)
        {
            throw new KeyNotFoundException("User not found!");
        }
        
        var existingProjectUser = await _context.ProjectUsers
            .FirstOrDefaultAsync(pu => pu.ProjectId == projectId && pu.UserId == userId);
        
        if (existingProjectUser == null)
        {
            throw new ArgumentException("There is no such user in the project!");
        }

        _context.ProjectUsers.Remove(existingProjectUser);
        await _context.SaveChangesAsync();
    }

    #endregion PUT METHODS


    #region DELETE METHODS

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

    #endregion DELETE METHODS
}