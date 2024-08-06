using CoinList.Infrastrcture;
using Common.Infrastructure.Installers;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallApplications(builder.Configuration, CoinList.Infrastrcture.AssemblyReference.Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

var app = builder.Build();

app.Services.RunAdditionalServices(builder.Configuration, CoinList.Infrastrcture.AssemblyReference.Assembly);

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

await app.RunAsync();
