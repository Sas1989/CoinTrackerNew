using Common.Application.Command;

namespace CoinList.Application.CoinUseCases.Create;

public sealed record CreateCoinCommand(string Symbol, string Name, decimal Price) : ICommand<Guid>
{
}
