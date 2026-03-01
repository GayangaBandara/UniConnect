using UniConnect.Api.Models;

namespace UniConnect.Api.Services.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(int userId);
    Task<RefreshToken?> ValidateAndGetAsync(string token);
    Task RevokeAsync(int userId);
}
