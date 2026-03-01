namespace backend.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string PasswordSalt { get; set; } = "";
    public string Role { get; set; } = "student"; // student | mentor | admin
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Expertise { get; set; } // Topics they mentor on
    public string? Bio { get; set; } // Profile description
    public bool IsAvailable { get; set; } = true; // Availability status
}
