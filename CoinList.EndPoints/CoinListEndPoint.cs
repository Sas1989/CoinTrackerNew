
using CoinList.Application.CoinUseCases.Create;
using Common.Application.Sender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoinList.EndPoints
{
    [ApiController]
    [Route("api/coin")]
    public class CoinListEndPoint : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        private readonly ILogger<CoinListEndPoint> logger;

        public CoinListEndPoint(IDispatcher dispatcher, ILogger<CoinListEndPoint> logger)
        {
            this.dispatcher = dispatcher;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateCoinCommand createCoin)
        {
            var result = await dispatcher.Send(createCoin);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            logger.LogInformation("Coin created: {CoinId}", result.Value);

            return Ok(result.Value);
        }

        [HttpGet]
        public IActionResult GetAsync() => Ok();

    }
}
