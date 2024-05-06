using CoinTracker.AcceptanceTest.Support.Models;
using System.Net.Http.Json;

namespace CoinTracker.AcceptanceTest.Support.CoinOperation;

internal sealed class CoinService
{
    private readonly HttpClient _httpClient;
    private const string ENDPOINT = "api/coin";

    public CoinService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateCoinAsync(CoinInput coinInput)
    {
        var response = await PostCoin(coinInput);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    public async Task<HttpResponseMessage> PostCoin(CoinInput coinInput)
    {
        return await _httpClient.PostAsJsonAsync(ENDPOINT, coinInput);
    }
}
