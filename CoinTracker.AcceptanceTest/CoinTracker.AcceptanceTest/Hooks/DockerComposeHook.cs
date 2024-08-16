using CoinTracker.AcceptanceTest.Support;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Extensions;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Net;

namespace CoinTracker.AcceptanceTest.Hooks;

[Binding]
internal static class DockerComposeHook
{
    private const string DockerComposeFileName = "docker-compose.yml";
    private const string ContainerBuilder = "ContainerBuilder";

    [BeforeTestRun]
    public static async Task StartDockerComposeAsync(TestThreadContext testContext) 
    {
        CreateAndStartContainer(testContext);
        await CreateConnectionToSqlContinaerAsync(testContext);

    }

    private static void CreateAndStartContainer(TestThreadContext testContext)
    {
        var composeFilePath = GetComposeFilePath();
        var containerBuilder = new Builder().UseContainer().UseCompose().FromFile(composeFilePath).RemoveOrphans().ForceBuild().Build().Start();
        testContext.Add(ContainerBuilder, containerBuilder);
    }

    private static string GetComposeFilePath()
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), DockerComposeFileName);
        var i = 0;
        Console.WriteLine(fullPath);
        while (!File.Exists(fullPath) && i < 100)
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory())
                ?? throw new FileNotFoundException($"{DockerComposeFileName} not found");

            Directory.SetCurrentDirectory(parent.FullName);
            fullPath = Path.Combine(Directory.GetCurrentDirectory(), DockerComposeFileName);
            Console.WriteLine(fullPath);
            i++;
        }
        Console.WriteLine("trovato" + fullPath);
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"{DockerComposeFileName} not found");
        return fullPath;

    }
    /*
    [AfterTestRun]
    public static void StopDockerCompose(TestThreadContext testContext)
    {
        var dbConnection = testContext.Get<DbConnection>(TestContainerKeys.DbConnection);
        dbConnection.Close();
        dbConnection.Dispose();

        var containerBuilder = testContext.Get<ICompositeService>(ContainerBuilder);

        containerBuilder.Stop();
        containerBuilder.Dispose();


    }
    */
    private static async Task CreateConnectionToSqlContinaerAsync(TestThreadContext testContext)
    {

        var sqlContainer = GetCoinatinerByName(testContext, "sql");

        var sqlAddress = GetEndPointFromPort(sqlContainer, "1433/tcp");

        var sqlPass = GetEnvVariableValue(sqlContainer,"SA_PASSWORD");

        var sqlConnectionString = new SqlConnectionStringBuilder
        {
            DataSource = $"localhost,{sqlAddress.Port}",
            UserID = "sa",
            Password = sqlPass,
            InitialCatalog = "CoinTracker",
            Encrypt = true,
            ConnectTimeout = 30,
            TrustServerCertificate = true,
            MultiSubnetFailover = true
        }.ConnectionString;

        var dbConnection = new SqlConnection(sqlConnectionString);
        await dbConnection.OpenAsync();

        testContext.Add(TestContainerKeys.DbConnection, dbConnection);
    }

    private static IContainerService GetCoinatinerByName(TestThreadContext testContext,string containerName)
    {
        var containerBuilder = testContext.Get<ICompositeService>(ContainerBuilder);
        var container = containerBuilder.Containers.FirstOrDefault(c => c.Name.Contains(containerName)) 
            ?? throw new FieldAccessException("SQL container not found");

        return container;
    }

    private static IPEndPoint GetEndPointFromPort(IContainerService container, string containerPort)
    {
        container.GetConfiguration(true);
        return container.ToHostExposedEndpoint(containerPort);
    }

    private static string GetEnvVariableValue(IContainerService container, string envVariable)
    {
        var envArray = container.GetConfiguration().Config.Env;
        var env = Array.Find(envArray, e => e.Contains(envVariable));

        if (string.IsNullOrEmpty(env) || !env.Contains('=')) throw new FieldAccessException($"{envVariable} variable not found");

        return env.Split("=")[1];
    }
}
