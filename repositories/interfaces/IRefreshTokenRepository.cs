using UniConnect.Api.Models;

namespace UniConnect.Api.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<bool> IsValidAsync(string token);
    Task AddAsync(RefreshToken refreshToken);
    Task RevokeAsync(int userId);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
