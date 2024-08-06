using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Installers;

public interface IApplicationInstaller : IInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration);
    void RunAdditionalService(IServiceProvider serviceProvider, IConfiguration configuration);
}
