﻿using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain;

namespace CoinList.Domain.CoinEntity;

public static class CoinError
{
    public readonly static Error NameIsEmpty = new("Coin.Name.IsEmpty", "Coin name cannot be empty");
    public readonly static Error SymbolIsEmpty = new("Coin.Symbol.IsEmpty", "Coin symbol cannot be empty");
    public readonly static Error PriceIsNegative = new("Coin.Price.IsNegative", "Coin price cannot be negative");
    public static Error CoinAlreadyExist(Symbol coinSymbol) => new("Coin.Already.Exist", $"Coin {coinSymbol.Value} already exists");
}
