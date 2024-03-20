using Common.Application.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.Installers;

internal static class InstallerHelper
{
    public static IEnumerable<T> GetInstallers<T>(params Assembly[] assemblies)
    {
        var InstallerList = assemblies.SelectMany(a => a.DefinedTypes).Where(ImplementInterface<T>).Select(Activator.CreateInstance).Cast<T>();

        return InstallerList;

    }

    internal static T GetService<T>()
    {
        return Activator.CreateInstance<T>();
    }

    private static bool ImplementInterface<T>(TypeInfo typeInfo)
    {
        return typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;
    } 
}
