using Application.Contracts.Data;
using Domain;

namespace Application.Mapping;

public static class DomainToDtoMapper
{
    public static CampaignDto ToCampaignDto(this Campaign campaign)
    {
        return new CampaignDto()
        {
            Id = campaign.Id.ToString(),
            LastModified = campaign.LastModified.Value.ToShortDateString(),
            Created = campaign.Created.Value.ToShortDateString(),
            Name = campaign.Name.Value,
            TagStatus = campaign.TagStatus.Value,
            Tags = campaign.Tags.Value,
            Status = campaign.Status.Value,
            Channels = campaign.Channels?.Select(channel => new ChannelDto()
            {
                Tags = channel.Tags.Value,
                LaunchDate = channel.LaunchDate.Value.ToShortDateString(),
                Title = channel.Title.Value
            })
        };
    }
}