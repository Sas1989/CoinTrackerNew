using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Common.Domain;

public static class Ensure
{
    public static Result NotNullOrEmpty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<string>(new Error("NotNullOrEmpty", $"{paramName} is null or empty"));
        }
        return Result.Success();
    }

    public static Result NotNegative(decimal value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0) 
        {
            return Result.Failure<decimal>(new Error("NotNullOrEmpty", $"{paramName} is negative"));
        }

        return Result.Success();
    }
}
