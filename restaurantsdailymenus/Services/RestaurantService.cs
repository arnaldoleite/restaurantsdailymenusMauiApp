using MongoDB.Driver;
using restaurantsdailymenus.Models;

namespace restaurantsdailymenus.Services;

public class RestaurantService
{
    private readonly IMongoCollection<Restaurant> _restaurants;

    public RestaurantService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDb:Database"]);
        _restaurants = database.GetCollection<Restaurant>("Restaurants");
    }

    public async Task<List<Restaurant>> GetAllAsync() =>
        await _restaurants.Find(_ => true).ToListAsync();

    public async Task<Restaurant?> GetByIdAsync(string id) =>
        await _restaurants.Find(r => r.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Restaurant r) =>
        await _restaurants.InsertOneAsync(r);

    public async Task UpdateAsync(string id, Restaurant updated) =>
        await _restaurants.ReplaceOneAsync(r => r.Id == id, updated);

    public async Task DeleteAsync(string id) =>
        await _restaurants.DeleteOneAsync(r => r.Id == id);

    public async Task<Restaurant?> GetByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        // Normalize same way you store/query (case-insensitive by name)
        var filter = Builders<Restaurant>.Filter.Eq(r => r.Name, name);

        return await _restaurants.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Restaurant?> GetByNameAsync(string id,string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;

        // Normalize same way you store/query (case-insensitive by name)
        var filter = Builders<Restaurant>.Filter.And(
            Builders<Restaurant>.Filter.Eq(r => r.Name, name),
            Builders<Restaurant>.Filter.Ne(r => r.Id, id));


        return await _restaurants.Find(filter).FirstOrDefaultAsync();
    }

}
