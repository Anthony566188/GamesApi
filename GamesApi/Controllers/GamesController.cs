using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using GamesApi.Models;
using GamesApi.Services;

namespace GamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly GameService _gameService;

    public GamesController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Game>>> GetAll() =>
        Ok(await _gameService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetById(string id)
    {
        if (!ObjectId.TryParse(id, out _))
            return BadRequest("Invalid id format.");

        var game = await _gameService.GetByIdAsync(id);

        if (game is null)
            return NotFound();

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> Create(Game newGame)
    {
        var created = await _gameService.CreateAsync(newGame);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Game>> Update(string id, Game updatedGame)
    {
        if (!ObjectId.TryParse(id, out _))
            return BadRequest("Invalid id format.");

        var game = await _gameService.UpdateAsync(id, updatedGame);

        if (game is null)
            return NotFound();

        return Ok(game);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (!ObjectId.TryParse(id, out _))
            return BadRequest("Invalid id format.");

        var deleted = await _gameService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}