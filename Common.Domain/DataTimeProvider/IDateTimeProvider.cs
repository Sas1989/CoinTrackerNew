namespace Common.Domain.DataTimeProvider;

public interface IDateTimeProvider
{
    public DateTime NowUtc { get; }
}
