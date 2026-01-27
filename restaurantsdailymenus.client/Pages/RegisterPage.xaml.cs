namespace restaurantsdailymenus.client.Pages;

using restaurantsdailymenus.client.Models;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}