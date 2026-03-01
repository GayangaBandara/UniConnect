using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<User>> GetAllAsync()
        => _db.Users.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

    public Task<User?> GetByIdAsync(int id)
        => _db.Users.FirstOrDefaultAsync(x => x.Id == id);

    public Task<User?> GetByEmailAsync(string email)
        => _db.Users.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<User> CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        return await _db.SaveChangesAsync() > 0;
    }
}
