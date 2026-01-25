using UniConnect.Api.Models;

namespace UniConnect.Api.Repositories.Interfaces;

public interface IGuidanceRequestRepository
{
    Task<List<GuidanceRequest>> GetAllAsync();
    Task<GuidanceRequest?> GetByIdAsync(int id);
    Task<GuidanceRequest> CreateAsync(GuidanceRequest request);
    Task<GuidanceRequest?> UpdateAsync(GuidanceRequest request);
    Task<bool> DeleteAsync(GuidanceRequest request);
}
