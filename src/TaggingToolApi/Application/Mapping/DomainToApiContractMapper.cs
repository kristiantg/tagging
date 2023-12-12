using Application.Contracts.Responses;
using Domain;
using Domain.DomainEvents;

namespace Application.Mapping;

public static class DomainToApiContractMapper
{
    public static CampaignResponse ToCampaignResponse(this Campaign campaign)
    {
        return new CampaignResponse(campaign.Id, 
            campaign.Name.Value, 
            campaign.Status.Value, 
            campaign.TagStatus.Value, 
            campaign.Tags.Value,
            campaign.Channels?.Select(channel => new ChannelResponse(
                    channel.Title.Value,
                    channel.LaunchDate.Value,
                    channel.Tags.Value
                )));
    }
}