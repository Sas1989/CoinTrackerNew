using Common.Application.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoinList.Infrastrcture.ServicesInstallers;

public class EndpointServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, Assembly assembly, IConfiguration configuration)
    {
        services.AddControllers().AddApplicationPart(assembly);
    }
}
