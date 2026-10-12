using GamesApi.Models;
using GamesApi.Repositories;

namespace GamesApi.Services;

public class GameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<List<Game>> GetAllAsync() =>
        await _gameRepository.GetAllAsync();

    public async Task<Game?> GetByIdAsync(string id) =>
        await _gameRepository.GetByIdAsync(id);

    public async Task<Game> CreateAsync(Game game)
    {
        // Garante que o banco de dados gere o ID, mesmo que o cliente envie um
        game.Id = null;

        await _gameRepository.CreateAsync(game);
        return game;
    }

    // Retorna o jogo atualizado, ou null se o ID não existir
    public async Task<Game?> UpdateAsync(string id, Game game)
    {
        var updated = await _gameRepository.UpdateAsync(id, game);
        return updated ? game : null;
    }

    public async Task<bool> DeleteAsync(string id) =>
        await _gameRepository.DeleteAsync(id);
}