
using CoinList.Application.CoinUseCases.Create;
using Common.Application.Sender;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CoinList.EndPoints
{
    [ApiController]
    [Route("api/coin")]
    public class CoinListEndPoint : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public CoinListEndPoint(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateCoinCommand createCoin)
        {
            var result = await dispatcher.Send(createCoin);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

    }
}
