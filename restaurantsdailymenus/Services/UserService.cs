using MongoDB.Driver;
using restaurantsdailymenus.Models;
using Microsoft.Extensions.Options;

namespace restaurantsdailymenus.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDb:Database"]);
        _users = database.GetCollection<User>(config["MongoDb:UsersCollection"]);
    }

    public async Task<User?> GetByUsernameAsync(string username) =>
        await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

    public async Task CreateAsync(User u) =>
        await _users.InsertOneAsync(u);
}
