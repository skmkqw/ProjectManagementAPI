using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectPulse.Core.Entities;
using ProjectPulse.Core.Models;
using ProjectPulse.DataAccess.Data;
using ProjectPulse.DataAccess.DTOs.Users;

namespace ProjectPulse.DataAccess.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    private readonly UserManager<AppUser> _userManager;

    public UsersRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    #region GET METHODS

    public async Task<IEnumerable<AppUser>> GetAll()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<AppUser?> GetById(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }
    
    public async Task<IEnumerable<ProjectTaskEntity>?> GetTasks(Guid userId)
    {
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
    
        if (userEntity == null) return null;
    
        return await _context.ProjectTasks.Where(t => t.AssignedUserId == userId).ToListAsync();
    }
    
    public async Task<IEnumerable<ProjectEntity>?> GetProjects(Guid userId)
    {
        var userEntity = await _userManager.FindByIdAsync(userId.ToString());
    
        if (userEntity == null) return null;
    
        return await _context.ProjectUsers
            .Where(pu => pu.UserId == userId)
            .Select(p => p.Project)
            .ToListAsync();
    }

    #endregion GET METHODS


    #region PUT METHODS

    public async Task<AppUser> Update(AppUser userEntity, UpdateUserDto updateUserDto)
    {
        userEntity.UserName = updateUserDto.UserName;
        userEntity.Email = updateUserDto.Email;
        userEntity.LastUpdateTime = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(userEntity);
        
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(userEntity);
            return userEntity;
        }
        throw new ApplicationException($"Unable to update user: {result.Errors}");
    }

    #endregion PUT METHODS


    #region DELETE METHODS

    public async Task<bool> Delete(Guid id)
    {
        var userEntity = await _userManager.FindByIdAsync(id.ToString());
        if (userEntity == null)
        {
            return false;
        }

        var result = await _userManager.DeleteAsync(userEntity);
        if (result.Succeeded)
        {
            return true;
        }
        throw new ApplicationException($"Unable to delete user: {result.Errors}");
    }

    #endregion DELETE METHODS
    
}