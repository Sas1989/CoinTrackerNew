using CoinList.Domain.CoinEntity.ValueObjects;

namespace CoinList.Domain.CoinEntity;

public interface ICoinRepository
{
    Task<Coin?> GetById(Guid coinId);
    Task<bool> CoinExistsBySymbol(Symbol symbol);
    void Add(Coin coin);
}
