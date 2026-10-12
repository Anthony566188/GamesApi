using Microsoft.Extensions.Options;
using MongoDB.Driver;
using GamesApi.Data;
using GamesApi.Models;

namespace GamesApi.Repositories;

public class GameRepository : IGameRepository
{
    private readonly IMongoCollection<Game> _gamesCollection;

    public GameRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
    {
        _gamesCollection = context.GetCollection<Game>(settings.Value.GamesCollectionName);
    }

    public async Task<List<Game>> GetAllAsync() =>
        await _gamesCollection.Find(_ => true).ToListAsync();

    public async Task<Game?> GetByIdAsync(string id) =>
        await _gamesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Game game) =>
        await _gamesCollection.InsertOneAsync(game);

    public async Task<bool> UpdateAsync(string id, Game game)
    {
        // O _id é imutável no MongoDB, então forçamos o valor proveniente da rota.
        game.Id = id;

        var result = await _gamesCollection.ReplaceOneAsync(x => x.Id == id, game);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _gamesCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}