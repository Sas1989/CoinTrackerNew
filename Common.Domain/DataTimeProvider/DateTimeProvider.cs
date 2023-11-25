namespace Common.Domain.DataTimeProvider;

public class DateTimeProvider : IDateTimeProvider
{
    private DateTimeProvider() { }
    public static IDateTimeProvider Current { get; set; } = new DateTimeProvider();

    public DateTime NowUtc => DateTime.UtcNow;
}