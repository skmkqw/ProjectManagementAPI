using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;

namespace ProjectManagement.DataAccess.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    #region GET METHODS

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _context.Users.AsNoTracking().Include(t => t.Tasks).ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _context.Users.Include(t => t.Tasks).FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<IEnumerable<ProjectTaskEntity>> GetTasks(Guid userId)
    {
        var userEntity = await _context.Users.FindAsync(userId);

        if (userEntity == null)
        {
            throw new KeyNotFoundException("User doesn't exist");
        }

        return await _context.ProjectTasks.Where(t => t.AssignedUserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<ProjectEntity>> GetProjects(Guid userId)
    {
        var userEntity = await _context.Users.FindAsync(userId);

        if (userEntity == null)
        {
            throw new KeyNotFoundException("User doesn't exist");
        }

        return await _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Include(p => p.Project.Tasks)
            .Include(pu => pu.Project.ProjectUsers)
            .ThenInclude(au => au.User)
            .Select(p => p.Project)
            .ToListAsync();
    }

    #endregion GET METHODS


    #region POST METHODS

    public async Task<UserEntity?> Create(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    #endregion POST METHODS


    #region PUT METHODS

    public async Task<UserEntity?> Update(UserEntity userEntity)
    {
        _context.Entry(userEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userEntity;
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task Delete(Guid id)
    {
        var userEntity = await _context.Users.FindAsync(id);
        if (userEntity == null)
        {
            throw new KeyNotFoundException("AssignedUser not found!");
        }
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
    }

    #endregion DELETE METHODS
    
}