using System.Security.Cryptography;
using System.Text;
using UniConnect.Api.Dtos.User;
using UniConnect.Api.Models;
using UniConnect.Api.Repositories.Interfaces;
using UniConnect.Api.Services.Interfaces;

namespace UniConnect.Api.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _repo;

    public UsersService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
        return users.Select(ToResponse).ToList();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        return user == null ? null : ToResponse(user);
    }

    public async Task<UserResponseDto?> GetByEmailAsync(string email)
    {
        var user = await _repo.GetByEmailAsync(email);
        return user == null ? null : ToResponse(user);
    }

    public async Task<UserResponseDto?> CreateAsync(UserCreateDto dto)
    {
        var exists = await _repo.GetByEmailAsync(dto.Email.Trim().ToLower());
        if (exists != null) return null;

        var user = new User
        {
            FullName = dto.FullName.Trim(),
            Email = dto.Email.Trim().ToLower(),
            PasswordHash = HashPassword(dto.Password),
            Role = dto.Role.Trim().ToLower()
        };

        var created = await _repo.CreateAsync(user);
        return ToResponse(created);
    }

    public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return null;

        user.FullName = dto.FullName.Trim();
        user.Role = dto.Role.Trim().ToLower();

        await _repo.UpdateAsync(user);
        return ToResponse(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repo.GetByIdAsync(id);
        if (user == null) return false;

        return await _repo.DeleteAsync(user);
    }

    private static UserResponseDto ToResponse(User u) => new()
    {
        Id = u.Id,
        FullName = u.FullName,
        Email = u.Email,
        Role = u.Role,
        CreatedAt = u.CreatedAt
    };

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }
}
