using Domain.Common;

namespace Application.Contracts.Responses;

public record CampaignResponse (Guid Id, string Name, string Status, int TagStatus, IEnumerable<string> Tags, IEnumerable<ChannelResponse>? Channels);