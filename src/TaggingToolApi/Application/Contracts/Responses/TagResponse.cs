using Domain.Common;

namespace Application.Contracts.Responses;

public class TagResponse
{
    public Guid Id { get; set; }
    public Guid TagId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid ChannelId { get; set; }
    public IEnumerable<string> Options { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime Created { get; set; }

    public TagResponse(Guid id, Guid tagId, Guid campaignId, Guid channelId, IEnumerable<string> options, DateTime lastModified, DateTime created)
    {
        Id = id;
        TagId = tagId;
        CampaignId = campaignId;
        ChannelId = channelId;
        Options = options;
        LastModified = lastModified;
        Created = created;
    }
}