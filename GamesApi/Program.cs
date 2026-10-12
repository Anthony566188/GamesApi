using GamesApi.Data;
using GamesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Scalar.AspNetCore;
using GamesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// O IMongoClient é thread-safe e gerencia o pool de conexões internamente
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var mongoSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
    mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
    return new MongoClient(mongoSettings);
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IGameRepository, GameRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Força a criação do contexto na inicialização, de modo que a mensagem de ping apareça no console
app.Services.GetRequiredService<MongoDbContext>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();