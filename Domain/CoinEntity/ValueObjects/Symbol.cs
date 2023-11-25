using Common.Domain;
using Common.Domain.DomainEntity;

namespace CoinList.Domain.CoinEntity.ValueObjects;

public sealed record Symbol : ValueObject<string, Symbol>
{
    protected override void ChangeValue()
    {
        Value = Value.ToUpperInvariant();
    }

    protected override Result Validate()
    {

        var result = Ensure.NotNullOrEmpty(Value);

        if (result.IsFailure)
        {
            return CoinError.SymbolIsEmpty;
        }

        return result;
    }

}