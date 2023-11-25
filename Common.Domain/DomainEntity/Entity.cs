namespace Common.Domain.DomainEntity;

public abstract class Entity
{
    public Guid Id { get; init; }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents  => _domainEvents.ToList();

    protected Entity(Guid id)
    {
        Id = id;
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

}
