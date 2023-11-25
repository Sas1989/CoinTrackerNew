namespace Common.Domain.DomainEntity;

public abstract record ValueObject<TBasicValue, TValueObject> where TValueObject : ValueObject<TBasicValue, TValueObject>, new()
{
    public TBasicValue Value { get; protected set; } = default!;

    public static Result<TValueObject> Create(TBasicValue value)
    {
        var valueObject = new TValueObject
        {
            Value = value
        };

        var result = valueObject.Validate();

        if (result.IsFailure)
        {

            return Result.Failure<TValueObject>(result.Error);
        }

        valueObject.ChangeValue();

        return valueObject;
    }

    protected abstract Result Validate();

    protected virtual void ChangeValue()
    {

    }
}