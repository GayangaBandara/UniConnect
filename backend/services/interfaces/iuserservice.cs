using backend.Dtos.User;

namespace backend.Services.Interfaces;

public interface IUsersService
{
    Task<List<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto?> GetByEmailAsync(string email);
    Task<UserResponseDto?> CreateAsync(UserCreateDto dto);
    Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
