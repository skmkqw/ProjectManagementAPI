using Microsoft.EntityFrameworkCore;
using ProjectManagementAPI.Data;
using ProjectManagementAPI.DTOs.Users;
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
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> Create(UserFromRequestDto userFromRequestDto)
    {
        var user = userFromRequestDto.ToUserFromRequestDto();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Update(int id, UserFromRequestDto userFromRequestDto)
    {
        var user = await GetById(id);
        
        if (user == null)
        {
            return null;
        }
        
        user.FirstName = userFromRequestDto.FirstName;
        user.LastName = userFromRequestDto.LastName;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User?> Delete(int id)
    {
        var user = await GetById(id);
        
        if (user == null)
        {
            return null;
        }
        
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return user;
    }
}