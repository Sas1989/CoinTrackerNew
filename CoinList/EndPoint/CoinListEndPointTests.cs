﻿using Castle.Core.Logging;
using CoinList.Application.CoinUseCases.Create;
using CoinList.EndPoints;
using Common.Application.Sender;
using Common.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoinList.Tests.EndPoint;

public class CoinListEndPointTests
{
    private readonly IDispatcher dispatcher;
    private readonly CoinListEndPoint endPoint;
    private readonly CreateCoinCommand createCommand;
    private readonly ILogger<CoinListEndPoint> logger;

    public CoinListEndPointTests()
    {
        dispatcher = Substitute.For<IDispatcher>();
        logger = Substitute.For<ILogger<CoinListEndPoint>>();
        endPoint = new CoinListEndPoint(dispatcher,logger);

        createCommand = new CreateCoinCommand("BTC", "Bitcoin", 1000);
    }

    [Fact]
    public async Task PostAsync_ReturnBadRequest_WhenResultIsFailure()
    {
        var result = Error.NullValue;

        dispatcher.Send(Arg.Any<CreateCoinCommand>()).Returns(result);

        var response = await endPoint.PostAsync(createCommand);

        var badRequest = response as BadRequestObjectResult;

        Assert.NotNull(badRequest);
        Assert.Equal(result, badRequest.Value);
    }

    [Fact]
    public async Task PostAsync_ReturnOk_WhenResultIsSuccess()
    {
        var result = Guid.NewGuid();

        dispatcher.Send(Arg.Any<CreateCoinCommand>()).Returns(result);

        var response = await endPoint.PostAsync(createCommand);

        var ok = response as OkObjectResult;

        Assert.NotNull(ok);
        Assert.Equal(result, ok.Value);
    }
}
