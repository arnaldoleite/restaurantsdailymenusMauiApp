using restaurantsdailymenus.client.Models;
using restaurantsdailymenus.client.Resources.Localization;
using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Devices.Sensors;
namespace restaurantsdailymenus.client.Models;
// ==================================
// RESTAURANT VIEWMODEL
// ==================================
[QueryProperty(nameof(RestaurantId), "RestaurantId")]
public class RestaurantViewModel : BaseViewModel, IQueryAttributable
{
    private readonly RestaurantsClient _client;


    public Restaurant Restaurant { get; set; } = new();
    public string RestaurantId { get; set; }


    public ICommand SaveCommand { get; }
    public ICommand GetLocalGPSCommand { get; }

    double _latitude;
    public double Latitude
    {
        get => _latitude;
        set => SetProperty(ref _latitude, value);
    }

    double _longitude;
    public double Longitude
    {
        get => _longitude;
        set => SetProperty(ref _longitude, value);
    }


    public RestaurantViewModel(RestaurantsClient client)
    {
        _client = client;
        SaveCommand = new Command(async () => await SaveAsync());
        GetLocalGPSCommand = new Command(async () => await GetLocalGPS());


    }

    /*executada automaticamente pelo MAUI Shel  
     * Shell.Current.GoToAsync(...) é chamado
     * O Shell cria a Page
     * O Shell cria o BindingContext (ViewModel)
     * ApplyQueryAttributes é chamada
     * OnAppearing() da Page é executado
     */
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("id"))
        {
            RestaurantId = query["id"].ToString();
            await LoadExisting(RestaurantId);
        }
    }


    private async Task LoadExisting(string id)
    {
        Restaurant = await _client.GetRestaurantAsync(id);
        OnPropertyChanged(nameof(Restaurant));
    }

    private async Task GetLocalGPS()
    {

        try
        {
            IsBusy = true;

            var request = new GeolocationRequest(
                GeolocationAccuracy.Medium,
                TimeSpan.FromSeconds(10));

            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                Restaurant.Latitude = location.Latitude;
                Restaurant.Longitude = location.Longitude;
            }
        }
        catch (PermissionException)
        {
            await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.error,
                AppResources.location_permission_denied,
                "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.error,
                ex.Message,
                "OK");
        }
        finally
        {
            IsBusy = false;
            OnPropertyChanged(nameof(Restaurant));
        }
    }


    private async Task SaveAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(Restaurant.Id))
            {
                await _client.CreateRestaurantAsync(Restaurant);
                await Application.Current.MainPage.DisplayAlertAsync(
                    AppResources.success,
                    AppResources.restaurant_created_successfully,
                    "OK");
            }
            else
            {
                await _client.UpdateRestaurantAsync(Restaurant.Id, Restaurant);
                await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.success,
                AppResources.restaurant_updated_successfully,
                "OK");
            }
            await Shell.Current.GoToAsync("..", true);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.error,
                ex.Message,
                "OK");
            return;
        }

        

    }
}