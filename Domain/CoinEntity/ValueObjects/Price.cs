using Common.Domain;
using Common.Domain.DomainEntity;

namespace CoinList.Domain.CoinEntity.ValueObjects;

public sealed record Price : ValueObject<decimal, Price>
{

    protected override Result Validate()
    {
        var result = Ensure.NotNegative(Value);
        if (result.IsFailure)
        {
            return CoinError.PriceIsNegative;
        }
        return result;
    }
}
