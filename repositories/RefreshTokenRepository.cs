using Microsoft.EntityFrameworkCore;
using UniConnect.Api.Data;
using UniConnect.Api.Models;
using UniConnect.Api.Repositories.Interfaces;

namespace UniConnect.Api.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<bool> IsValidAsync(string token)
    {
        var refreshToken = await GetByTokenAsync(token);

        if (refreshToken == null)
            return false;

        if (refreshToken.IsRevoked)
            return false;

        if (refreshToken.ExpiryDate < DateTime.UtcNow)
            return false;

        return true;
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task RevokeAsync(int userId)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.IsRevoked = true;
        }

        _context.RefreshTokens.UpdateRange(tokens);
    }

    public async Task DeleteAsync(int id)
    {
        var token = await _context.RefreshTokens.FindAsync(id);
        if (token != null)
        {
            _context.RefreshTokens.Remove(token);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
