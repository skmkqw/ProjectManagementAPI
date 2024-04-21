using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Users;
using ProjectManagementAPI.Entities;
using ProjectManagementAPI.Mappers;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAll()
    {
        var userEntities = await _context.Users.ToListAsync();
        var users = userEntities.Select(x => User.Create(x.Id, x.FirstName, x.LastName));
        return users;
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userEntity == null)
        {
            return null;
        }

        User.Create(userEntity.Id, userEntity.FirstName, userEntity.LastName);

        return userEntity;
    }

    public async Task<UserEntity?> Create(UserFromRequestDto userFromRequest)
    {
        var userEntity = userFromRequest.ToUserEntity();
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    public async Task<UserEntity?> Update(Guid id, UserFromRequestDto userFromRequest)
    {
        var user = await GetById(id);
        
        if (user == null)
        {
            return null;
        }
        
        user.FirstName = userFromRequest.FirstName;
        user.LastName = userFromRequest.LastName;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<int> Delete(Guid id)
    {
        int isDeleted = await _context.Users.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return isDeleted;
    }
}