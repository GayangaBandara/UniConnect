using Microsoft.AspNetCore.Mvc;
using UniConnect.Api.DTOs.Auth;
using UniConnect.Api.Models;
using UniConnect.Api.Services;
using UniConnect.Api.Services.Interfaces;

namespace UniConnect.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IUsersService _usersService;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;

    public AuthController(
        IAuthenticationService authService,
        IUsersService usersService,
        ILogger<AuthController> logger,
        IConfiguration configuration)
    {
        _authService = authService;
        _usersService = usersService;
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
            // Create user with hashed password
            var (hash, salt) = _authService.HashPassword(request.Password);

            var userDto = new UserCreateDto
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            var user = await _usersService.CreateAsync(userDto);

            // Update password hash and salt (since UserService hashes it differently)
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            // Generate tokens
            var accessToken = _authService.GenerateAccessToken(user);
            var refreshToken = _authService.GenerateRefreshToken();

            _logger.LogInformation("User registered: {Email}, Role: {Role}", user.Email, user.Role);

            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = expirationMinutes,
                User = new UserAuthDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
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
            var user = await _usersService.GetByEmailAsync(request.Email);

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
            var refreshToken = _authService.GenerateRefreshToken();

            _logger.LogInformation("User logged in: {Email}", user.Email);

            var expirationMinutes = int.Parse(_configuration["JwtSettings:ExpirationMinutes"] ?? "30");

            return Ok(new LoginResponseDto
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
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
            // In a production app, you would validate the refresh token against a database
            // For now, this is a simplified implementation
            // TODO: Implement refresh token validation and storage

            return BadRequest(new { success = false, message = "Refresh token feature coming soon" });
        }
        catch (Exception ex)
        {
            _logger.LogError("Refresh token error: {Message}", ex.Message);
            return StatusCode(500, new { success = false, message = "Internal server error" });
        }
    }
}
