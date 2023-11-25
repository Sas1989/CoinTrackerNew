using Common.Domain.DataTimeProvider;
using Common.Domain.DomainEntity;

namespace Common.Tests.DomainEntity;

public class AuditableEntityTests
{
    public AuditableEntityTests()
    {

    }

    [Fact]
    public void Constructor_Should_FilledCreateTimeAndUpdateDate()
    {
        DateTime actualTime = SetDateTime();

        var entity = new TestAuditableEntity(Guid.NewGuid());

        Assert.Equal(actualTime, entity.CreateTime);
        Assert.Equal(actualTime, entity.UpdateTime);
    }

    [Fact]
    public void Update_Should_UpdateUpdateTime()
    {

        var entity = new TestAuditableEntity(Guid.NewGuid());
        DateTime actualTime = SetDateTime();

        entity.Update();


        Assert.True(entity.CreateTime < entity.UpdateTime);
        Assert.Equal(actualTime, entity.UpdateTime);
    }

    private static DateTime SetDateTime()
    {
        var provider = Substitute.For<IDateTimeProvider>();
        var actualTime = DateTime.UtcNow.AddSeconds(10);
        provider.NowUtc.Returns(actualTime);
        DateTimeProvider.Current = provider;
        return actualTime;
    }
}
