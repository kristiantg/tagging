using Domain.Abstractions;
using Domain.Common;

namespace Domain.DomainEvents;

public sealed record CampaignUpdatedDomainEvent(Guid Id) : IDomainEvent;