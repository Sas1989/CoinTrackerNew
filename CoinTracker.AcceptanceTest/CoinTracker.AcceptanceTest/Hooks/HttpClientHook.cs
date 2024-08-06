
using BoDi;

namespace CoinTracker.AcceptanceTest.Hooks;

[Binding]
internal sealed class HttpClientHook
{
    private readonly IObjectContainer _objectContainer;

    public HttpClientHook(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void CreateHttpClient()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:28080") };
       
       _objectContainer.RegisterInstanceAs(httpClient);
    }
}
