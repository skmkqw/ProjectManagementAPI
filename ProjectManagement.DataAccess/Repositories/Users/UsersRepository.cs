using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Models;
using ProjectManagement.DataAccess.Data;
using ProjectManagement.DataAccess.DTOs.Users;
using ProjectManagement.DataAccess.Entities;
using ProjectManagement.DataAccess.Mappers;

namespace ProjectManagement.DataAccess.Repositories.Users;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _context;

    public UsersRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserEntity>> GetAll()
    {
        return await _context.Users.AsNoTracking().Include(t => t.Tasks).ToListAsync();
    }

    public async Task<UserEntity?> GetById(Guid id)
    {
        return await _context.Users.Include(t => t.Tasks).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<UserEntity?> Create(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity;
    }

    public async Task<UserEntity?> Update(UserEntity userEntity)
    {
        _context.Entry(userEntity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return userEntity;
    }

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
}