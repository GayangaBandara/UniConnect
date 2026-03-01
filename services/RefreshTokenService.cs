using UniConnect.Api.Models;
using UniConnect.Api.Repositories.Interfaces;
using UniConnect.Api.Services.Interfaces;

namespace UniConnect.Api.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RefreshTokenService> _logger;

    public RefreshTokenService(
        IRefreshTokenRepository repository,
        IConfiguration configuration,
        ILogger<RefreshTokenService> logger)
    {
        _repository = repository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
    {
        var refreshTokenExpiryDays = int.Parse(_configuration["JwtSettings:RefreshTokenExpiryDays"] ?? "7");

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = GenerateRefreshToken(),
            ExpiryDate = DateTime.UtcNow.AddDays(refreshTokenExpiryDays)
        };

        await _repository.AddAsync(refreshToken);
        await _repository.SaveChangesAsync();

        _logger.LogInformation("Refresh token created for user: {UserId}", userId);
        return refreshToken;
    }

    public async Task<RefreshToken?> ValidateAndGetAsync(string token)
    {
        var isValid = await _repository.IsValidAsync(token);

        if (!isValid)
        {
            _logger.LogWarning("Invalid refresh token attempted");
            return null;
        }

        return await _repository.GetByTokenAsync(token);
    }

    public async Task RevokeAsync(int userId)
    {
        await _repository.RevokeAsync(userId);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Refresh tokens revoked for user: {UserId}", userId);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
