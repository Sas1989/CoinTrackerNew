using CoinList.Application.CoinUseCases.Create;
using CoinList.Domain.CoinEntity;
using CoinList.Domain.CoinEntity.ValueObjects;
using Common.Domain.Persistance;

namespace CoinList.Tests.Application.CoinUseCases.Create;

public class CreateCoinHandlerTests
{
    private ICoinRepository coinRepository;
    private IUnitOfWork unitOfWork;
    private CreateCoinHandler handler;
    private CreateCoinCommand command;

    public CreateCoinHandlerTests()
    {
        coinRepository = Substitute.For<ICoinRepository>();
        unitOfWork = Substitute.For<IUnitOfWork>();
        handler = new CreateCoinHandler(coinRepository, unitOfWork);

        command = new CreateCoinCommand("BTC", "Bitcoin", 1000);
    }

    [Fact]
    public async Task Handle_ReturnError_WhenSymbolIsEmptyAsync()
    {
        var emptySymbolCommand = new CreateCoinCommand(string.Empty, "Bitcoin", 1000);

        var result = await handler.Handle(emptySymbolCommand, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(CoinError.SymbolIsEmpty,result.Error);
    }

    [Fact]
    public async Task Handle_ReturnError_WhenNameIsEmptyAsync()
    {
        var emptyNameCommand = new CreateCoinCommand("BTC", string.Empty, 1000);

        var result = await handler.Handle(emptyNameCommand, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(CoinError.NameIsEmpty, result.Error);
    }

    [Fact]
    public async Task Handle_ReturnError_WhenPriceIsNegativeAysnc()
    {
        var negativePriceCommand = new CreateCoinCommand("BTC", "Bitcoin", -1000);

        var result = await handler.Handle(negativePriceCommand, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(CoinError.PriceIsNegative, result.Error);
    }

    [Fact]
    public async Task Handle_ReturnError_WhenCoinAlreadyExist()
    {
        coinRepository.CoinExistsBySymbol(Arg.Any<Symbol>()).Returns(true);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(CoinError.CoinAlreadyExist(Symbol.Create(command.Symbol).Value), result.Error);
    }

    [Fact]
    public async Task Handle_AddCoinToRepository_AndSave()
    {

        var result = await handler.Handle(command, CancellationToken.None);

        coinRepository.Received(1).Add(Arg.Any<Coin>());
        await unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handle_ReturnCoinId()
    {

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }
}
