using restaurantsdailymenus.client.Models;
using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
namespace restaurantsdailymenus.client.Models;
// ==================================
// RESTAURANTS LIST VIEWMODEL
// ==================================
public class RestaurantsViewModel : BaseViewModel
{
    private readonly RestaurantsClient _client;
    Restaurant _selectedRestaurant;

    bool _isRefreshing;

    public ObservableCollection<Restaurant> Restaurants { get; } = new();

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            _isRefreshing = value;
            OnPropertyChanged(nameof(IsRefreshing));
        }
    }

    public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var restaurant = e.CurrentSelection.FirstOrDefault();
        Console.WriteLine("SELECTION CHANGED (code-behind)");
    }
    public Restaurant SelectedRestaurant
    {
        get => _selectedRestaurant;

        set
        {
            if (_selectedRestaurant == value)
                return;

            _selectedRestaurant = value;
            OnPropertyChanged(nameof(SelectedRestaurant));

            if (_selectedRestaurant != null)
                OpenRestaurant(_selectedRestaurant);
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddRestaurantCommand { get; }
    public ICommand RefreshCommand { get; }

    public RestaurantsViewModel(RestaurantsClient client)
    {
        _client = client;
        LoadCommand = new Command(async () => await LoadAsync());
        AddRestaurantCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("restaurant");
        });
        RefreshCommand = new Command(async () =>
        {
            IsRefreshing = true;
            await LoadAsync();
            IsRefreshing = false;
        });
    }

    public async Task LoadAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Restaurants.Clear();
            var list = await _client.GetRestaurantsAsync();
            if (list != null)
            {
                foreach (var r in list)
                    Restaurants.Add(r);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    //private async void OpenRestaurant(Restaurant r)
    //{
    //    var route = $"restaurant?id={r.Id}";
    //    await Shell.Current.GoToAsync(route);
    //}

    async void OpenRestaurant(Restaurant r)
    {
        await Shell.Current.GoToAsync(
            "restaurant",
            new Dictionary<string, object>
            {
            { "id", r.Id }
            });

        SelectedRestaurant = null; // MUITO IMPORTANTE
    }
}