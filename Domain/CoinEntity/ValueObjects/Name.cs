using Common.Domain;
using Common.Domain.DomainEntity;
using System.Net.Http.Headers;

namespace CoinList.Domain.CoinEntity.ValueObjects;

public sealed record Name : ValueObject<string, Name>
{

    protected override Result Validate()
    {
        var result = Ensure.NotNullOrEmpty(Value);

        if (result.IsFailure)
        {
            return CoinError.NameIsEmpty;
        }

        return result;
    }
}
