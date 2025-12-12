using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace restaurantsdailymenus.client.Models;

public class LoginViewModel
{
    private readonly AuthClient _auth;
    private readonly TokenService _tokenService;

    public LoginViewModel(AuthClient auth, TokenService tokenService)
    {
        _auth = auth;
        _tokenService = tokenService;
    }

    public async Task<bool> Login(string user, string pass)
    {
        var response = await _auth.LoginAsync(
            new LoginDto { Username = user, Password = pass });

        // response should contain token
        await _tokenService.SaveTokenAsync(response.Token);

        return true;
    }
}
public class RestaurantsViewModel
{
    private readonly RestaurantsClient _client;

    public ObservableCollection<Restaurant> Restaurants { get; } = new();

    public RestaurantsViewModel(RestaurantsClient client)
    {
        _client = client;
    }

    public async Task LoadAsync()
    {
        var list = await _client.GetRestaurantsAsync();
        Restaurants.Clear();
        foreach (var r in list)
            Restaurants.Add(r);
    }
}


