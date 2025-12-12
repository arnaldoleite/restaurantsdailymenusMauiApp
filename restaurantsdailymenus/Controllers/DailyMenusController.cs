
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using restaurantsdailymenus.Models;
using restaurantsdailymenus.Services;

namespace restaurantsdailymenus.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/menus")]
public class DailyMenusController : ControllerBase
{
    private readonly DailyMenuService _menus;
    private readonly RestaurantService _restaurants;

    public DailyMenusController(DailyMenuService menus, RestaurantService restaurants)
    {
        _menus = menus;
        _restaurants = restaurants;
    }

    // GET all menus for a restaurant
    [HttpGet]
    public async Task<IActionResult> GetAll(string restaurantId)
    {
        var items = await _menus.GetAllForRestaurant(restaurantId);
        return Ok(items);
    }

    // GET menu for a specific date
    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetMenuByDate(string restaurantId, DateTime date)
    {
        var menu = await _menus.GetMenuByDate(restaurantId, date);
        if (menu == null) return NotFound();
        return Ok(menu);
    }

    // POST new daily menu
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(string restaurantId, DailyMenu menu)
    {
        // ensure restaurant exists
        var rest = await _restaurants.GetByIdAsync(restaurantId);
        if (rest == null) return NotFound("Restaurant not found");

        menu.RestaurantId = restaurantId;
        await _menus.CreateAsync(menu);
        return Ok(menu);
    }

    // PUT update a menu
    [Authorize]
    [HttpPut("{menuId}")]
    public async Task<IActionResult> Update(string restaurantId, string menuId, DailyMenu menu)
    {
        var existing = await _menus.GetByIdAsync(menuId);
        if (existing == null) return NotFound();

        menu.Id = menuId;
        menu.RestaurantId = restaurantId;

        await _menus.UpdateAsync(menuId, menu);
        return Ok(menu);
    }

    // DELETE a menu
    [Authorize]
    [HttpDelete("{menuId}")]
    public async Task<IActionResult> Delete(string restaurantId, string menuId)
    {
        var existing = await _menus.GetByIdAsync(menuId);
        if (existing == null) return NotFound();

        await _menus.DeleteAsync(menuId);
        return NoContent();
    }
}
