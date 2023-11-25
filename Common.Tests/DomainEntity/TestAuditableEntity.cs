using Common.Domain.DomainEntity;

namespace Common.Tests.DomainEntity;

internal sealed class TestAuditableEntity : AuditAbleEntity
{
    public TestAuditableEntity(Guid id) : base(id)
    {
    }

    public void Update() => base.SetUpdateTime();
}
