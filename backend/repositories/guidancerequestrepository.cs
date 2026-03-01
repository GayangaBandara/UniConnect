using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;

namespace backend.Repositories;

public class GuidanceRequestRepository : IGuidanceRequestRepository
{
    private readonly AppDbContext _db;

    public GuidanceRequestRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<List<GuidanceRequest>> GetAllAsync()
        => _db.GuidanceRequests.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();

    public Task<GuidanceRequest?> GetByIdAsync(int id)
        => _db.GuidanceRequests.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<GuidanceRequest> CreateAsync(GuidanceRequest request)
    {
        _db.GuidanceRequests.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<GuidanceRequest?> UpdateAsync(GuidanceRequest request)
    {
        _db.GuidanceRequests.Update(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<bool> DeleteAsync(GuidanceRequest request)
    {
        _db.GuidanceRequests.Remove(request);
        return await _db.SaveChangesAsync() > 0;
    }
}
