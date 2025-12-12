using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace restaurantsdailymenus.Models;

public class DailyMenu
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string Id { get; set; } = null!;
    public string RestaurantId { get; set; } = null!;   // FK
    public DateTime Date { get; set; }
    public string Item1 { get; set; } = "";
    public decimal Price1 { get; set; }
    public string Item2 { get; set; } = "";
    public decimal Price2 { get; set; }
    public string Item3 { get; set; } = "";
    public decimal Price3 { get; set; }
    public string Notes { get; set; } = "";
}