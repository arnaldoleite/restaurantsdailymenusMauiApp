using restaurantsdailymenus.client.Models;
namespace restaurantsdailymenus.client.Pages;

public partial class LoginPage : ContentPage
{
    
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
}

