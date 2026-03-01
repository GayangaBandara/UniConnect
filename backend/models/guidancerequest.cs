namespace backend.Models;

public class GuidanceRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "pending"; // pending | accepted | closed

    public int StudentId { get; set; }
    public int? MentorId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? Student { get; set; }
    public User? Mentor { get; set; }
}
