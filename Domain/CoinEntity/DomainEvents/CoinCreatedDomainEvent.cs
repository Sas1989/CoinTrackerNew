using Common.Domain.DomainEntity;

namespace CoinList.Domain.CoinEntity.DomainEvents;

public sealed record CoinCreatedDomainEvent(Guid Id) : IDomainEvent;