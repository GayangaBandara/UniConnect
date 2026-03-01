using backend.Models;

namespace backend.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(int userId);
    Task<RefreshToken?> ValidateAndGetAsync(string token);
    Task RevokeAsync(int userId);
}
