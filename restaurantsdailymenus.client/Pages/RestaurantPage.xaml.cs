using restaurantsdailymenus.client.Models;

namespace restaurantsdailymenus.client.Pages;

public partial class RestaurantPage : ContentPage
{
    public RestaurantPage(RestaurantViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}