using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Entities;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Users;

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
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<IEnumerable<ProjectTaskEntity>?> GetTasks(Guid userId)
    {
        var userEntity = await _context.Users.FindAsync(userId);

        if (userEntity == null) return null;

        return await _context.ProjectTasks.Where(t => t.AssignedUserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<ProjectEntity>?> GetProjects(Guid userId)
    {
        var userEntity = await _context.Users.FindAsync(userId);

        if (userEntity == null) return null;

        return await _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Select(p => p.Project)
            .ToListAsync();
    }

    #endregion GET METHODS


    #region POST METHODS

    public async Task<UserEntity> Create(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    #endregion POST METHODS


    #region PUT METHODS

    public async Task<UserEntity> Update(UserEntity userEntity, UpdateUserDto updateUserDto)
    {
        _context.Entry(userEntity).CurrentValues.SetValues(updateUserDto);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task<bool> Delete(Guid id)
    {
        var userEntity = await _context.Users.FindAsync(id);
        if (userEntity == null)
        {
            return false;
        }
        _context.Users.Remove(userEntity);
        await _context.SaveChangesAsync();
        return true;                                                                                       
    }

    #endregion DELETE METHODS
    
}