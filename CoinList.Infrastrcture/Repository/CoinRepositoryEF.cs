using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using CoinList.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CoinList.Infrastructure.Repository
{
    internal sealed class CoinRepositoryEF : ICoinRepository
    {
        private readonly CoinListDbContext coinListDbContext;

        public CoinRepositoryEF(CoinListDbContext coinListDbContext)
        {
            this.coinListDbContext = coinListDbContext;
        }

        public void Add(Coin coin)
        {
            coinListDbContext.Coin.Add(coin);
        }

        public async Task<bool> CoinExistsBySymbol(Symbol symbol)
        {
            return await coinListDbContext.Coin.AnyAsync(c => c.Symbol == symbol);
        }

        public Task<Coin?> GetById(Guid coinId)
        {
            return coinListDbContext.Coin.FirstOrDefaultAsync(c => c.Id == coinId);
        }
    }
}
