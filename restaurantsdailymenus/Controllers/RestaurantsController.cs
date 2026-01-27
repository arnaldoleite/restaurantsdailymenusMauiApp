using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using restaurantsdailymenus.Models;
using restaurantsdailymenus.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace restaurantsdailymenus.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _restaurants;

    public RestaurantsController(RestaurantService service)
    {
        _restaurants = service;
    }

    // GET: api/restaurants
    [Authorize] // remove if you want public creation
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _restaurants.GetAllAsync();
        return Ok(items);
    }

    // GET: api/restaurants/{id}
    [Authorize] // remove if you want public creation
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var restaurant = await _restaurants.GetByIdAsync(id);
        if (restaurant == null) return NotFound();

        return Ok(restaurant);
    }

    // POST: api/restaurants
    [Authorize] // remove if you want public creation
    [HttpPost]
    public async Task<IActionResult> Create(Restaurant restaurant)
    {
        if (restaurant == null) return BadRequest("Restaurant payload is required.");
        if (string.IsNullOrWhiteSpace(restaurant.Name)) return BadRequest("Restaurant name is required.");

        // Normalize input values for comparison
        var incomingName = restaurant.Name.Trim();

        // Check for duplicate by name using a targeted query
        var existingByName = await _restaurants.GetByNameAsync(incomingName);
        if (existingByName != null)
            return Conflict("A restaurant with the same name already exists.");
        
        await _restaurants.CreateAsync(restaurant);
        return Ok(restaurant);
    }

    // PUT: api/restaurants/{id}
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Restaurant restaurant)
    {
        var existing = await _restaurants.GetByIdAsync(id);
        if (existing == null) return NotFound();

        if (restaurant == null) return BadRequest("Restaurant payload is required.");
        if (string.IsNullOrWhiteSpace(restaurant.Name)) return BadRequest("Restaurant name is required.");

        // Normalize input values for comparison
        var incomingName = restaurant.Name.Trim();

        // Check for duplicate by name using a targeted query
        var existingByName = await _restaurants.GetByNameAsync(id,incomingName);
        if (existingByName != null)
            return Conflict("A restaurant with the same name already exists.");

        restaurant.Id = id;

        await _restaurants.UpdateAsync(id, restaurant);
        return Ok(restaurant);
    }

    // DELETE: api/restaurants/{id}
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existing = await _restaurants.GetByIdAsync(id);
        if (existing == null) return NotFound();

        await _restaurants.DeleteAsync(id);
        return Ok();
    }
}
