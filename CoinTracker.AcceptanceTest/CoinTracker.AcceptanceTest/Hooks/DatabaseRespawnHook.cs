using BoDi;
using CoinTracker.AcceptanceTest.Support;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using FluentAssertions.Execution;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Respawn;
using System.Data;
using System.Data.Common;

namespace CoinTracker.AcceptanceTest.Hooks;

[Binding]
internal sealed class DatabaseRespawnHook 
{
    private readonly string[] SchemasList = new[] { "CoinList" };
    private const string RespawnerKey = "respawner";

    [BeforeScenario]
    public async Task SetRespawnAsync(TestThreadContext testContext)
    {
        if (!testContext.ContainsKey(RespawnerKey))
        {
            var dbConnection = await GetOpenDbConnectionAsync(testContext);
            var respawner = await Respawner.CreateAsync(dbConnection, new RespawnerOptions { SchemasToInclude = SchemasList });
            testContext.Add("respawner", respawner);
        }
    }

    [AfterScenario]
    public async Task ResetDatabaseAsync(TestThreadContext testContext)
    {
        var dbConnection = await GetOpenDbConnectionAsync(testContext);

        var respawner = testContext.Get<Respawner>(RespawnerKey);

        await respawner.ResetAsync(dbConnection);
    }


    public async Task<DbConnection> GetOpenDbConnectionAsync(TestThreadContext testContext)
    {
        var dbConnection = testContext.Get<DbConnection>(TestContainerKeys.DbConnection);

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        return dbConnection;
    }
}
