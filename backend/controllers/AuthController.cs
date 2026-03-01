using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Auth;
using backend.Dtos.User;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IUsersService _usersService;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;

    public AuthController(
        IAuthenticationService authService,
        IUsersService usersService,
        IUserRepository userRepository,
        IRefreshTokenService refreshTokenService,
        ILogger<AuthController> logger,
        IConfiguration configuration)
    {
        _authService = authService;
        _usersService = usersService;
        _userRepository = userRepository;
        _refreshTokenService = refreshTokenService;
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Register a new user (student or mentor)
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        // Validate passwords match
        if (request.Password != request.ConfirmPassword)
        {
            return BadRequest(new { success = false, message = "Passwords do not match" });
        }

        // Validate password strength
        if (!_authService.IsValidPassword(request.Password, out var passwordError))
        {
            return BadRequest(new { success = false, message = passwordError });
        }

        // Validate role
        if (request.Role != "student" && request.Role != "mentor")
        {
            return BadRequest(new { success = false, message = "Role must be 'student' or 'mentor'" });
        }

        try
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email.Trim().ToLower());
            if (existingUser != null)
            {
                return BadRequest(new { success = false, message = "Email already exists" });
            }

            // Create user with hashed password
            var (hash, salt) = _authService.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim().ToLower(),
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = request.Role.Trim().ToLower(),
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await _userRepository.CreateAsync(user);

            // Generate tokens
            var accessToken = _authService.GenerateAccessToken(createdUser);
            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(createdUser.Id);

            _logger.LogInformation("User registered: {Email}, Role: {Role}", createdUser.Email, createdUser.Role);

            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = expirationMinutes,
                User = new UserAuthDto
                {
                    Id = createdUser.Id,
                    FullName = createdUser.FullName,
                    Email = createdUser.Email,
                    Role = createdUser.Role
                },
                Message = "Registration successful"
            });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Registration failed: {Message}", ex.Message);
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError("Registration error: {Message}", ex.Message);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Login with email and password
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }

        try
        {
            // Find user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login attempt with non-existent email: {Email}", request.Email);
                return Unauthorized(new { success = false, message = "Invalid email or password" });
            }

            // Verify password
            if (!_authService.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                _logger.LogWarning("Failed login attempt for user: {Email}", user.Email);
                return Unauthorized(new { success = false, message = "Invalid email or password" });
            }

            // Generate tokens
            var accessToken = _authService.GenerateAccessToken(user);
            var refreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.Id);

            _logger.LogInformation("User logged in: {Email}", user.Email);

            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = expirationMinutes,
                User = new UserAuthDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                },
                Message = "Login successful"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Login error: {Message}", ex.Message);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }

    /// <summary>
    /// Refresh access token using refresh token
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
        {
            return BadRequest(new { success = false, message = "Refresh token is required" });
        }

        try
        {
            // Validate refresh token
            var refreshToken = await _refreshTokenService.ValidateAndGetAsync(request.RefreshToken);

            if (refreshToken == null)
            {
                _logger.LogWarning("Invalid refresh token attempted");
                return Unauthorized(new { success = false, message = "Invalid or expired refresh token" });
            }

            // Get user
            var user = await _userRepository.GetByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                _logger.LogWarning("User not found for refresh token: {UserId}", refreshToken.UserId);
                return Unauthorized(new { success = false, message = "User not found" });
            }

            // Generate new access token
            var newAccessToken = _authService.GenerateAccessToken(user);
            var newRefreshToken = await _refreshTokenService.CreateRefreshTokenAsync(user.Id);

            _logger.LogInformation("Token refreshed for user: {Email}", user.Email);

            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Success = true,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresIn = expirationMinutes,
                User = new UserAuthDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                },
                Message = "Token refreshed successfully"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError("Refresh token error: {Message}", ex.Message);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }
}
