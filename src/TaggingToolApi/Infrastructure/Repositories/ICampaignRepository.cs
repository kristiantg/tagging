using Application.Contracts.Data;
using Application.Contracts.Requests;

namespace Infrastructure.Repositories;

public interface ICampaignRepository
{
    Task<bool> CreateAsync(CampaignDto campaign);

    Task<CampaignDto?> GetAsync(Guid id);

    Task<IEnumerable<CampaignDto>> GetAllAsync(GetCampaignsQuery query);

    Task<bool> UpdateAsync(CampaignDto campaign);

    Task<bool> DeleteAsync(Guid id);
}