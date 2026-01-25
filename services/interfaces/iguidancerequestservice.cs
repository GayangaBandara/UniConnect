using UniConnect.Api.Dtos.GuidanceRequest;

namespace UniConnect.Api.Services.Interfaces;

public interface IGuidanceRequestService
{
    Task<List<RequestResponseDto>> GetAllAsync();
    Task<RequestResponseDto?> GetByIdAsync(int id);
    Task<RequestResponseDto> CreateAsync(RequestCreateDto dto);
    Task<RequestResponseDto?> UpdateAsync(int id, RequestUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
