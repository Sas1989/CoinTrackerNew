namespace Common.Domain;

public record Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(Error error) : base(false, error) { }
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }
    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Value not accessible in case of error");

    public static implicit operator Result<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);
}