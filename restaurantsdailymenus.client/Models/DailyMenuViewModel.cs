using restaurantsdailymenus.client.Models;
using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
namespace restaurantsdailymenus.client.Models;
// ==================================
// DAILY MENUS VIEWMODEL
// ==================================
[QueryProperty(nameof(RestaurantId), "RestaurantId")]
public class DailyMenuViewModel : BaseViewModel, IQueryAttributable
{
    private readonly DailyMenusClient _client;


    public ObservableCollection<DailyMenu> Menus { get; } = new();
    public string RestaurantId { get; set; }

    public ICommand AddMenuCommand { get; }

    public DailyMenuViewModel(DailyMenusClient client)
    {
        _client = client;
        AddMenuCommand = new Command(async () => await AddMenuAsync());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        RestaurantId = query["id"].ToString();
        await LoadAsync();
    }

    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Menus.Clear();
            var list = await _client.GetMenusAsync(RestaurantId);
            if (list != null)
            {
                foreach (var m in list)
                    Menus.Add(m);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task AddMenuAsync()
    {
        var newMenu = new DailyMenu
        {
            RestaurantId = RestaurantId,
            Date = DateTime.Now,
            Item1 = "New Item",
            Price1 = 0
        };

        await _client.CreateMenuAsync(RestaurantId, newMenu);
        await LoadAsync();
    }
}

