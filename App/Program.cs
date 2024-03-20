using Common.Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallApplications(builder.Configuration,CoinList.Infrastrcture.AssemblyReference.Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseAuthorization();

app.MapControllers();

app.Run();
