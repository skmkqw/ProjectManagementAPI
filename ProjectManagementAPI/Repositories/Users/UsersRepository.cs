using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.Entities;
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

    public async Task<User?> GetById(Guid id)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userEntity == null)
        {
            return null;
        }

        return User.Create(userEntity.Id, userEntity.FirstName, userEntity.LastName);
    }

    public async Task<UserEntity?> Create(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    public async Task<UserEntity?> Update(Guid id, UserEntity userEntity)
    {
        var user = await GetById(id);
        
        if (user == null)
        {
            return null;
        }
        
        user.FirstName = userEntity.FirstName;
        user.LastName = userEntity.LastName;
        _context.Users.Update(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }
    public async Task<int> Delete(Guid id)
    {
        int isDeleted = await _context.Users.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return isDeleted;
    }
}