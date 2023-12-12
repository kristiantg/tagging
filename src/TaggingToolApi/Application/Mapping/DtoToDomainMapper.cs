using Application.Contracts.Data;
using Domain;
using Domain.Common;
using Domain.DomainEvents;

namespace Application.Mapping;

public static class DtoToDomainMapper
{
    public static Campaign ToCampaign(this CampaignDto campaignDto)
    {
        return new Campaign(Guid.Parse(campaignDto.Id),
            new Name(campaignDto.Name),
            new Status(campaignDto.Status),
            new TagStatus(campaignDto.TagStatus),
            new Tags(campaignDto.Tags),
            new LastModified(DateTime.Parse(campaignDto.LastModified)),
            new Created(DateTime.Parse(campaignDto.Created)),
            campaignDto.Channels?.Select(dto => new Channel(
                new Title(dto.Title),
                new Tags(dto.Tags),
                new LaunchDate(DateTime.Parse(dto.LaunchDate))
                ))
        );
    }
}