using CoinTracker.AcceptanceTest.Support.CoinOperation;
using CoinTracker.AcceptanceTest.Support.Models;
using System.Net;
using System.Net.Http.Json;
using TechTalk.SpecFlow.Assist;

namespace CoinTracker.AcceptanceTest.StepDefinitions;

[Binding]
internal sealed class CreateCoinStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly CoinService _coinService;

    private const string NEW_COIN_KEY = "NewCoin";
    private const string RESPONSE_KEY = "response";
    private const string EXISTING_COIN_KEY = "ExistingCoin";
    private const string EXISTING_COIN_ID_KEY = "ExistingCoinId";

    public CreateCoinStepDefinitions(ScenarioContext scenarioContext, CoinService coinService)
    {
        _scenarioContext = scenarioContext;
        _coinService = coinService;
    }

    [Given(@"A new coin with the following properties")]
    public void GivenANewCoinWithTheFollowingProperties(Table table)
    {
        var coinInput = table.CreateInstance<CoinInput>();
        _scenarioContext.Add(NEW_COIN_KEY, coinInput);
    }

    [When(@"I create the coin")]
    public async Task WhenICreateTheCoinAsync()
    {
        var coinInput = _scenarioContext.Get<CoinInput>(NEW_COIN_KEY);
        var response = await _coinService.PostCoin(coinInput);
        _scenarioContext.Add(RESPONSE_KEY, response);
    }

    [Then(@"I get a guid")]
    public void ThenIGetAGuid()
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(RESPONSE_KEY);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var coinId = response.Content.ReadFromJsonAsync<Guid>().Result;
        coinId.Should().NotBe(Guid.Empty);
    }

    [Then(@"I have an error message ""([^""]*)""")]
    public async Task ThenIHaveAnErrorMessageAsync(string p0)
    {
        var response = _scenarioContext.Get<HttpResponseMessage>(RESPONSE_KEY);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var errorMessage = await response.Content.ReadFromJsonAsync<Error>();

        errorMessage.Should().NotBeNull();
        errorMessage?.Message.Should().Be(p0);
    }
    [Given(@"An existing coin with the following properties")]
    public async Task GivenAnExistingCoinWithTheFollowingPropertiesAsync(Table table)
    {
        var coinInput = table.CreateInstance<CoinInput>();
        _scenarioContext.Add(EXISTING_COIN_KEY, coinInput);
        var coinId = await _coinService.CreateCoinAsync(coinInput);
        _scenarioContext.Add(EXISTING_COIN_ID_KEY, coinId);
        
    }

    [When(@"I create a new coin with the same symbol")]
    public async Task WhenICreateANewCoinWithTheSameSymbol()
    {
        var coinInput = _scenarioContext.Get<CoinInput>(EXISTING_COIN_KEY);
        var newCoin = new CoinInput(coinInput.Symbol, "Test", 10000);
        var response = await _coinService.PostCoin(newCoin);
        _scenarioContext.Add(RESPONSE_KEY, response);
    }

}
