using GamesApi.Models;

namespace GamesApi.Repositories;

public interface IGameRepository
{
    Task<List<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(string id);
    Task CreateAsync(Game game);
    Task<bool> UpdateAsync(string id, Game game);
    Task<bool> DeleteAsync(string id);
    Task<List<Game>> SearchByPlatformAndMaxPriceAsync(string platform, decimal maxPrice);
    Task<List<PlatformStockReport>> GetStockReportAsync();
}