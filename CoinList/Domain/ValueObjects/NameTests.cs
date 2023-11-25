using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain;

namespace CoinList.Tests.Domain.ValueObjects;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_Should_ThrowArgumentException_WhenNameIsInvalid(string name)
    {
        Result<Name> coinName = Name.Create(name);

        Assert.True(coinName.IsFailure);
        Assert.Equal(CoinError.NameIsEmpty, coinName.Error);
    }

    [Fact]
    public void Constructor_Should_SaveValue()
    {
        var name = "name";

        Result<Name> coinName = Name.Create(name);

        Assert.True(coinName.IsSuccess);
        Assert.Equal(name, coinName.Value?.Value);
    }
}
