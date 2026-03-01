namespace backend.Dtos.GuidanceRequest;

public class RequestCreateDto
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int StudentId { get; set; }
}
