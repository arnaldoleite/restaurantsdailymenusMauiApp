
using RestaurantsDailyMenus.Api;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace restaurantsdailymenus.client.Models;
// ==================================
// RESTAURANT DETAILS VIEWMODEL
// ==================================
public class RestaurantDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly RestaurantsClient _client;

    public Restaurant Restaurant { get; private set; }

    public ICommand OpenDailyMenusCommand { get; }

    public RestaurantDetailsViewModel(RestaurantsClient client)
    {
        _client = client;
        OpenDailyMenusCommand = new Command(async () => await OpenDailyMenus());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        string id = query["id"].ToString();
        await LoadAsync(id);
    }

    private async Task LoadAsync(string id)
    {
        Restaurant = await _client.GetRestaurantAsync(id);
        OnPropertyChanged(nameof(Restaurant));
    }

    private async Task OpenDailyMenus()
    {
        await Shell.Current.GoToAsync($"dailymenu?id={Restaurant.Id}");
    }
}


