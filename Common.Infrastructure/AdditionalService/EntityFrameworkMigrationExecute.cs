using Common.Application.Installers;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Infrastructure.AdditionalService;
public class EntityFrameworkMigrationExecute<T> : IAdditionalService where T : DbContext
{
    public void Run(IServiceProvider serviceProvider, Assembly assembly, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<T>();
        context.Database.Migrate();

    }
}
