using restaurantsdailymenus.client.Models;

namespace restaurantsdailymenus.client.Pages;

public partial class DailyMenuPage : ContentPage
{
    public DailyMenuPage(DailyMenuViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}