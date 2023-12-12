using Domain;
using Domain.Common;

namespace Application.Contracts.Requests;

public record CreateCampaignRequest(string Name, string Status, int TagStatus, IEnumerable<string> Tags, IEnumerable<CreateChannelRequest>? Channels);