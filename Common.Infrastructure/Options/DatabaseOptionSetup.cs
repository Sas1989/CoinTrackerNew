using Common.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Options;

internal sealed class DatabaseOptionSetup : IConfigureOptions<DatabaseOptions>
{
    private readonly IConfiguration _configuration;

    public DatabaseOptionSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public void Configure(DatabaseOptions options)
    {
        var connection = _configuration.GetConnectionString("Database");
        if (connection == null)
        {
            throw new ConfigurationException("Database connection string not found");
        }

        options.ConnectionString = connection;
        _configuration.GetSection("DatabseOptions").Bind(options);
    }
}
