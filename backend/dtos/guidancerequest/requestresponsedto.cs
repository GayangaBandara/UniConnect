namespace backend.Dtos.GuidanceRequest;

public class RequestResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "";
    public int StudentId { get; set; }
    public int? MentorId { get; set; }
    public DateTime CreatedAt { get; set; }
}
