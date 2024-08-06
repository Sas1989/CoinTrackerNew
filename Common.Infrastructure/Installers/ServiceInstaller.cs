using Common.Application.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.Installers;

public static class ServiceInstaller
{
    public static IServiceCollection InstallService<T>(this IServiceCollection services, Assembly assembly, IConfiguration configuration) where T : IServiceInstaller
    {
        var installer = InstallerHelper.GetService<T>();

        installer.Install(services, assembly, configuration);

        return services;
    }

    public static IServiceProvider RunAdditionlService<T>(this IServiceProvider serviceProvider, Assembly assembly, IConfiguration configuration) where T : IAdditionalService
    {
        var provider = InstallerHelper.GetService<T>();

        provider.Run(serviceProvider,assembly, configuration);

        return serviceProvider;
    }
}
