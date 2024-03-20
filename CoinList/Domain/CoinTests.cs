using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.DomainEvents;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain.DataTimeProvider;

namespace CoinList.Tests.Domain;

public class CoinTests
{
    private readonly Symbol symbol;
    private readonly Name name;
    private readonly Price price;

    public CoinTests()
    {
        symbol = Symbol.Create("BTC").Value!;
        name = Name.Create("Bitcoin").Value!;
        price = Price.Create(34307.63M).Value;
    }

    [Fact]
    public void Create_Should_CreateCoinNotNull()
    {
        var coin = Coin.Create(symbol, name, price);
        
        Assert.NotNull(coin);
    }

    [Fact]
    public void Create_Should_CreateCoin()
    {

        var coin = Coin.Create(symbol, name, price);

        Assert.NotEqual(default,coin.Id);
        Assert.Equal(symbol.Value,coin.Symbol.Value);
        Assert.Equal(name.Value,coin.Name.Value);
        Assert.Equal(price.Value,coin.Price.Value);
    }
    [Fact]
    public void Create_Should_RaiseDomainEvent()
    {
        var coin = Coin.Create(symbol, name, price);

        var expectedEvent = new CoinCreatedDomainEvent(coin.Id);

        Assert.Equal(1, coin.DomainEvents.Count);
        Assert.Contains(expectedEvent,coin.DomainEvents);
    }

}
