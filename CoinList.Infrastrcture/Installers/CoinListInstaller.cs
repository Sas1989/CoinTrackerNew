using CoinList.Domain.CoinEntity;
using CoinList.Infrastrcture.Repository;
using CoinList.Infrastrcture.ServicesInstallers;
using Common.Application.Installers;
using Common.Domain.Persistance;
using Common.Infrastructure.Installers;
using Common.Infrastructure.ServicesInstallers;
using Common.Infrastructure.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Infrastructure.AdditionalService;

namespace CoinList.Infrastrcture;

public sealed class CoinListInstaller : IApplicationInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.InstallService<DispatcherInstaller>(Application.AssemblyReference.Assembly, configuration);
        services.InstallService<EndpointServiceInstaller>(EndPoints.AssemblyReference.Assembly, configuration);
        services.InstallService<EntityFrameworkSqlServerInstaller<CoinListDbContext>>(AssemblyReference.Assembly, configuration);
        services.InstallService<OpenTelemetryServiceInstaller>(AssemblyReference.Assembly, configuration);
        InstallRepository(services);
    }

    private static void InstallRepository(IServiceCollection services)
    {
        services.AddScoped<ICoinRepository, CoinRepositoryEF>();
        services.AddScoped<IUnitOfWork, UnitOfWork<CoinListDbContext>>();
    }

    public void RunAdditionalService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        serviceProvider.RunAdditionlService<EntityFrameworkMigrationExecute<CoinListDbContext>>(Application.AssemblyReference.Assembly, configuration);
    }
}
