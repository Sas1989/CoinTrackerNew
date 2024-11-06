using Common.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

        _configuration.GetSection("DatabseOptions").Bind(options);

        if (options.Server.IsNullOrEmpty())
        {
            throw new ConfigurationException("Server Cannot Be Empy");
        }

        if (options.DatabaseName.IsNullOrEmpty())
        {
            throw new ConfigurationException("DatabaseName Cannot Be Empty");
        }
    }
}
