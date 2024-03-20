using Common.Application.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.Installers;

public static class ApplicatonInstaller
{
    public static IServiceCollection InstallApplications(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        var installers = InstallerHelper.GetInstallers<IApplicationInstaller>(assemblies);

        foreach (var installer in installers)
        {
            installer.Install(services, configuration);
        }

        return services;
    }
}