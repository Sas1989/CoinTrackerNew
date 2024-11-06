using Common.Application.Installers;
using Common.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Common.Infrastructure.ServicesInstallers;

public sealed class EntityFrameworkSqlServerInstaller<TContext> : IServiceInstaller where TContext : DbContext
{
    public void Install(IServiceCollection services, Assembly assembly, IConfiguration configuration)
    {

        services.ConfigureOptions<DatabaseOptionSetup>();

        services.AddDbContext<TContext>((seviceProvider, cfg) =>
        {
            var databaseOptions = seviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
            cfg.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>
            {
                sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);

                cfg.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                cfg.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }).LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        });
        
    }
}
