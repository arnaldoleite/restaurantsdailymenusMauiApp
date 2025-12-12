using MongoDB.Driver;
using restaurantsdailymenus.Models;

namespace restaurantsdailymenus.Services;

public class DailyMenuService
{
    private readonly IMongoCollection<DailyMenu> _menus;

    public DailyMenuService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDb:ConnectionString"]);
        var db = client.GetDatabase(config["MongoDb:Database"]);
        _menus = db.GetCollection<DailyMenu>("DailyMenus");
    }

    public async Task<List<DailyMenu>> GetAllForRestaurant(string restaurantId) =>
        await _menus.Find(m => m.RestaurantId == restaurantId).ToListAsync();

    public async Task<DailyMenu?> GetByIdAsync(string id) =>
        await _menus.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(DailyMenu menu) =>
        await _menus.InsertOneAsync(menu);

    public async Task UpdateAsync(string id, DailyMenu updated) =>
        await _menus.ReplaceOneAsync(m => m.Id == id, updated);

    public async Task DeleteAsync(string id) =>
        await _menus.DeleteOneAsync(m => m.Id == id);

    public async Task<DailyMenu?> GetMenuByDate(string restaurantId, DateTime date) =>
        await _menus.Find(m => m.RestaurantId == restaurantId && m.Date.Date == date.Date)
                    .FirstOrDefaultAsync();
}
