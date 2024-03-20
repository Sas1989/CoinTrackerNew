using CoinList.Domain.CoinEntity.DomainEvents;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain.DomainEntity;
using System.ComponentModel;

namespace CoinList.Domain.CoinEntity;

public sealed class Coin : AuditAbleEntity
{
    private Coin(Guid id, Symbol symbol, Name name, Price price) : base(id)
    {
        Symbol = symbol;
        Name = name;
        Price = price;
    }

    public Symbol Symbol { get; }
    public Name Name { get; }
    public Price Price { get; }

    public static Coin Create(Symbol symbol, Name name, Price price)
    {
        var coin = new Coin(Guid.NewGuid(), symbol, name, price);

        coin.Raise(new CoinCreatedDomainEvent(coin.Id));

        return coin;

    }
}
