using System.Runtime.CompilerServices;

namespace CoinTracker.AcceptanceTest.Support;

internal static class Ensure
{
    public static void NotNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (object.Equals(value, default(T)))
        {
            throw new ArgumentNullException($"{paramName} is null");
        }
    }
}
