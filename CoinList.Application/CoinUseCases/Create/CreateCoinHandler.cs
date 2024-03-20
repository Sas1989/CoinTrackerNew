using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Application.Command;
using Common.Domain;
using Common.Domain.Persistance;

namespace CoinList.Application.CoinUseCases.Create;

public sealed class CreateCoinHandler : ICommandHandler<CreateCoinCommand, Guid>
{
    private readonly ICoinRepository _coinRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCoinHandler(ICoinRepository coinRepository, IUnitOfWork unitOfWork)
    {
        _coinRepository = coinRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCoinCommand request, CancellationToken cancellationToken)
    {
        var symbol = Symbol.Create(request.Symbol);
        var name = Name.Create(request.Name);
        var price = Price.Create(request.Price);

        if (symbol.IsFailure)
        {
            return symbol.Error;
        }

        if (name.IsFailure)
        {
            return name.Error;
        }

        if (price.IsFailure)
        {
            return price.Error;
        }

        if (await _coinRepository.CoinExistsBySymbol(symbol.Value))
        {
            return CoinError.CoinAlreadyExist(symbol.Value);
        }

        var coin = Coin.Create(symbol.Value, name.Value, price.Value);

        _coinRepository.Add(coin);

        await _unitOfWork.SaveChangesAsync(); 

        return coin.Id;

    }
}
