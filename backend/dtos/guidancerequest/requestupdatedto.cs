namespace backend.Dtos.GuidanceRequest;

public class RequestUpdateDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "pending"; // pending | accepted | closed
    public int? MentorId { get; set; }
}
