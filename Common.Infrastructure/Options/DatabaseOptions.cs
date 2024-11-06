using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Common.Infrastructure.Options;

public class DatabaseOptions
{
    public string ConnectionString
    {
        get
        {
            var connectionSting = $"Server={Server};Database={DatabaseName};";


            if (!User.IsNullOrEmpty())
            {
                connectionSting += $";User Id={User}";
            }

            if (!Secret.IsNullOrEmpty())
            {
                connectionSting += $";Password={Secret}";
            }

            if (!AdditionalOptions.IsNullOrEmpty())
            {
                connectionSting += $";{AdditionalOptions}";
            }

            return connectionSting;
        }
    }


    public string Secret { get; set; } = string.Empty;
    public string Server {  get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string AdditionalOptions {  get; set; } = string.Empty;
    public int MaxRetryCount { get; set; } = 3;
    public int CommandTimeout { get; set; } = 30;
    public bool EnableSensitiveDataLogging { get; set; }
    public bool EnableDetailedErrors { get; set; }
}
