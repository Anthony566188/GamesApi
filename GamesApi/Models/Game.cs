using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GamesApi.Models;

public class Game
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    [Required]
    public string Title { get; set; } = null!;

    [BsonElement("platform")]
    [Required]
    public string Platform { get; set; } = null!;

    [BsonElement("genre")]
    [Required]
    public string Genre { get; set; } = null!;

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [BsonElement("releaseYear")]
    [Range(1950, 2100)]
    public int ReleaseYear { get; set; }

    [BsonElement("stock")]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}