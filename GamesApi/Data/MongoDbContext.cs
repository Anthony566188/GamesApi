using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using GamesApi.Models;

namespace GamesApi.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        // Teste de conexão (ping) com o MongoDB
        try
        {
            client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged MongoDB. Connection successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
        }

        // Inicializa o banco de dados usando o nome do appsettings
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    // Método genérico para pegar qualquer coleção
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}