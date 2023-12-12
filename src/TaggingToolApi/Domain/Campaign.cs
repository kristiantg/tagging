using Domain.Abstractions;
using Domain.Common;
using Domain.DomainEvents;

namespace Domain;

public class Campaign : Entity
{
    public Name Name { get; private set; }
    public Status Status { get; private set; }
    public TagStatus TagStatus { get; private set; }
    public Tags Tags { get; private set; }
    public LastModified LastModified { get; private set; }
    public Created Created { get; private set; }
    public IEnumerable<Channel>? Channels { get; set; }

    public Campaign(Guid id, Name name, Status status, TagStatus tagStatus, Tags tags, LastModified lastModified, Created created, IEnumerable<Channel>? channels) : base(id)
    {
        Name = name;
        Status = status;
        TagStatus = tagStatus;
        Tags = tags;
        LastModified = lastModified;
        Created = created;
        Channels = channels;
    }

    public static Campaign Create(Name name, Status status, TagStatus tagStatus, Tags tags, IEnumerable<Channel>? channels, LastModified lastModified, Created created)
    {
        var campaign = new Campaign(Guid.NewGuid(), 
            name, 
            status, 
            tagStatus, 
            tags, 
            lastModified, 
            created,
            channels
            );
        
        campaign.Raise(new CampaignCreatedDomainEvent(campaign.Id));
        
        return campaign;
    }

    public static Guid Delete(Campaign campaign)
    {
        campaign.Raise(new CampaignDeletedDomainEvent(campaign.Id));
        return campaign.Id;
    }

    public static Campaign CreateUpdate(Guid guid, Name name, Status status, TagStatus tagStatus, Tags tags, IEnumerable<Channel>? channels, LastModified lastModified, Created created)
    {
        var campaign = new Campaign(guid, 
            name, 
            status, 
            tagStatus, 
            tags, 
            lastModified, 
            created,
            channels);
        
        campaign.Raise(new CampaignUpdatedDomainEvent(guid));

        return campaign;
    }
}