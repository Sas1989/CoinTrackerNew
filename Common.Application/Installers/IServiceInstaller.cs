using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Application.Installers;

public interface IServiceInstaller : IInstaller
{
    void Install(IServiceCollection services, Assembly assembly, IConfiguration configuration);
}
