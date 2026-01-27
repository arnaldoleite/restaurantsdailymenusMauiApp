using restaurantsdailymenus.client.Models;

namespace restaurantsdailymenus.client.Pages;

public partial class RestaurantsPage : ContentPage
{
    public RestaurantsPage(RestaurantsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RestaurantsViewModel vm)
            await vm.LoadAsync();
    }
}