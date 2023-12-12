using Domain.Abstractions;
using Domain.Common;

namespace Domain.DomainEvents;

public sealed record CampaignDeletedDomainEvent(Guid Id) : IDomainEvent;