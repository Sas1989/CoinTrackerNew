using Common.Domain.DataTimeProvider;

namespace Common.Domain.DomainEntity;

public abstract class AuditAbleEntity : Entity
{
    public DateTime CreateTime { get; init; }
    public DateTime UpdateTime { get; private set; }

    protected AuditAbleEntity(Guid id) : base(id)
    {
        CreateTime = DateTimeProvider.Current.NowUtc;
        UpdateTime = DateTimeProvider.Current.NowUtc;
    }

    protected void SetUpdateTime()
    {
        UpdateTime = DateTimeProvider.Current.NowUtc;
    }

}
