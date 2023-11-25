using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain;

namespace CoinList.Tests.Domain.ValueObjects;

public class PriceTests
{
    [Fact]
    public void Constructor_Should_ThrowArgumentException_WhenPriceIsInvalid()
    {
        Random random = new();
        decimal priceValue = (decimal)(random.NextDouble() * -1.0);
        Result<Price> priceResult = Price.Create(priceValue);

        Assert.True(priceResult.IsFailure);
        Assert.Equal(CoinError.PriceIsNegative, priceResult.Error);
    }

    [Fact]
    public void Constructor_Should_MakeValueUpperCase()
    {
        Random random = new();
        decimal priceValue = (decimal)random.NextDouble();

        Result<Price> priceResult = Price.Create(priceValue);

        Assert.True(priceResult.IsSuccess);
        Assert.Equal(priceValue, priceResult.Value.Value);
    }
}
