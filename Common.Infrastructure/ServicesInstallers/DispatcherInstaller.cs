using Common.Application.Installers;
using Common.Application.Sender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.ServicesInstallers;

public sealed class DispatcherInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, Assembly assembly, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddTransient<IDispatcher, Dispatcher>();
    }
}
