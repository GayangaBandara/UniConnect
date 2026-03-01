namespace backend.DTOs.Auth;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int ExpiresIn { get; set; } // in minutes
    public UserAuthDto? User { get; set; }
    public string? Message { get; set; }
}

public class UserAuthDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
}
