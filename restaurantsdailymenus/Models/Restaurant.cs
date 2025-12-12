

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace restaurantsdailymenus.Models;

public class Restaurant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Description { get; set; } = "";
    public string LogoUrl { get; set; } = "";
    public string BackgroundUrl { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}