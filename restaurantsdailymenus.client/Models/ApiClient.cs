// Auto-generated API client for restaurantsdailymenus API (simplified NSwag-style)
// NOTE: This is a fully typed client based on your swagger.json
// Drop this into your MAUI project under /Services/Api/ApiClient.cs

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestaurantsDailyMenus.Api
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _http;
        protected readonly JsonSerializerOptions _json;

        protected BaseClient(HttpClient http)
        {
            _http = http;
            _json = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        protected async Task<T?> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var str = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(str)) return default;
            return JsonSerializer.Deserialize<T>(str, _json);
        }

        protected StringContent SerializeBody(object body)
        {
            return new StringContent(JsonSerializer.Serialize(body, _json), Encoding.UTF8, "application/json");
        }
    }

    // =============================
    // AUTH CLIENT
    // =============================
    public class AuthClient : BaseClient
    {
        public AuthClient(HttpClient http) : base(http) { }

        public async Task<object?> RegisterAsync(RegisterDto dto)
        {
            var response = await _http.PostAsync("api/v1/Auth/register", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }

        public async Task<TokenResponse?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsync("api/v1/Auth/login", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<TokenResponse>(response);
        }
    }

    // =============================
    // RESTAURANTS CLIENT
    // =============================
    public class RestaurantsClient : BaseClient
    {
        public RestaurantsClient(HttpClient http) : base(http) { }

        public async Task<List<Restaurant>?> GetRestaurantsAsync()
        {
            var response = await _http.GetAsync("api/v1/Restaurants");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<List<Restaurant>>(response);
        }

        public async Task<Restaurant?> GetRestaurantAsync(string id)
        {
            var response = await _http.GetAsync($"api/v1/Restaurants/{id}");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<Restaurant>(response);
        }

        public async Task<object?> CreateRestaurantAsync(Restaurant dto)
        {
            var response = await _http.PostAsync("api/v1/Restaurants", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }

        public async Task<object?> UpdateRestaurantAsync(string id, Restaurant dto)
        {
            var response = await _http.PutAsync($"api/v1/Restaurants/{id}", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }

        public async Task<object?> DeleteRestaurantAsync(string id)
        {
            var response = await _http.DeleteAsync($"api/v1/Restaurants/{id}");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }
    }

    // =============================
    // DAILY MENUS CLIENT
    // =============================
    public class DailyMenusClient : BaseClient
    {
        public DailyMenusClient(HttpClient http) : base(http) { }

        public async Task<List<DailyMenu>?> GetMenusAsync(string restaurantId)
        {
            var response = await _http.GetAsync($"api/restaurants/{restaurantId}/menus");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<List<DailyMenu>>(response);
        }

        public async Task<DailyMenu?> GetMenuByDateAsync(string restaurantId, DateTime date)
        {
            string d = date.ToString("o");
            var response = await _http.GetAsync($"api/restaurants/{restaurantId}/menus/date/{d}");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<DailyMenu>(response);
        }

        public async Task<object?> CreateMenuAsync(string restaurantId, DailyMenu dto)
        {
            var response = await _http.PostAsync($"api/restaurants/{restaurantId}/menus", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }

        public async Task<object?> UpdateMenuAsync(string restaurantId, string menuId, DailyMenu dto)
        {
            var response = await _http.PutAsync($"api/restaurants/{restaurantId}/menus/{menuId}", SerializeBody(dto));
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }

        public async Task<object?> DeleteMenuAsync(string restaurantId, string menuId)
        {
            var response = await _http.DeleteAsync($"api/restaurants/{restaurantId}/menus/{menuId}");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }
    }

    // =============================
    // TEST SECURE ENDPOINT
    // =============================
    public class TestClient : BaseClient
    {
        public TestClient(HttpClient http) : base(http) { }

        public async Task<object?> SecureTestAsync()
        {
            var response = await _http.GetAsync("api/v1/Test/secure");
            response.EnsureSuccessStatusCode();
            return await DeserializeAsync<object>(response);
        }
    }

    // =============================
    // DTOs
    // =============================

    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class RegisterDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class TokenResponse
    {
        public string? Token { get; set; }
    }

    public class Restaurant
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public string? BackgroundUrl { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class DailyMenu
    {
        public string? Id { get; set; }
        public string? RestaurantId { get; set; }
        public DateTime Date { get; set; }
        public string? Item1 { get; set; }
        public double Price1 { get; set; }
        public string? Item2 { get; set; }
        public double Price2 { get; set; }
        public string? Item3 { get; set; }
        public double Price3 { get; set; }
        public string? Notes { get; set; }
    }
}
