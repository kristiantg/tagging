using Domain.Common;

namespace Application.Contracts.Requests;

public record UpdateCampaignRequest(Guid CampaignId, string Name, string Status, int TagStatus, IEnumerable<string> Tags, IEnumerable<UpdateChannelRequest>? Channels);