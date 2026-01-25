using UniConnect.Api.Dtos.GuidanceRequest;
using UniConnect.Api.Models;
using UniConnect.Api.Repositories.Interfaces;
using UniConnect.Api.Services.Interfaces;

namespace UniConnect.Api.Services;

public class GuidanceRequestService : IGuidanceRequestService
{
    private readonly IGuidanceRequestRepository _repo;
    private readonly IUserRepository _users;

    public GuidanceRequestService(IGuidanceRequestRepository repo, IUserRepository users)
    {
        _repo = repo;
        _users = users;
    }

    public async Task<List<RequestResponseDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(ToResponse).ToList();
    }

    public async Task<RequestResponseDto?> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item == null ? null : ToResponse(item);
    }

    public async Task<RequestResponseDto> CreateAsync(RequestCreateDto dto)
    {
        // basic check: student exists
        var student = await _users.GetByIdAsync(dto.StudentId);
        if (student == null) throw new ArgumentException("student not found");

        var req = new GuidanceRequest
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Status = "pending",
            StudentId = dto.StudentId
        };

        var created = await _repo.CreateAsync(req);
        return ToResponse(created);
    }

    public async Task<RequestResponseDto?> UpdateAsync(int id, RequestUpdateDto dto)
    {
        var req = await _repo.GetByIdAsync(id);
        if (req == null) return null;

        req.Title = dto.Title.Trim();
        req.Description = dto.Description.Trim();
        req.Status = dto.Status.Trim().ToLower();
        req.MentorId = dto.MentorId;

        await _repo.UpdateAsync(req);
        return ToResponse(req);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var req = await _repo.GetByIdAsync(id);
        if (req == null) return false;

        return await _repo.DeleteAsync(req);
    }

    private static RequestResponseDto ToResponse(GuidanceRequest r) => new()
    {
        Id = r.Id,
        Title = r.Title,
        Description = r.Description,
        Status = r.Status,
        StudentId = r.StudentId,
        MentorId = r.MentorId,
        CreatedAt = r.CreatedAt
    };
}
