namespace restaurantsdailymenus.client;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();


        // Register navigation routes
        Routing.RegisterRoute("restaurantdetails", typeof(Pages.RestaurantDetailsPage));
        Routing.RegisterRoute("dailymenu", typeof(Pages.DailyMenuPage));
        Routing.RegisterRoute("restaurants", typeof(Pages.RestaurantsPage));
        Routing.RegisterRoute("restaurant", typeof(Pages.RestaurantPage));
        Routing.RegisterRoute("register", typeof(Pages.RegisterPage));
    }
}

// =============================
// App.xaml.cs (STARTUP NAVIGATION)
// =============================


/*
public partial class App : Application
{
public App(AppShell shell)
{
InitializeComponent();
MainPage = shell;


// Start app on Login page
Shell.Current.GoToAsync("//login");
}
}
*/


// =============================
// NAVIGATION FLOW SUMMARY
// =============================
// LoginPage -> //restaurants
// RestaurantsPage -> restaurantdetails?id={id}
// RestaurantDetails -> dailymenu?id={restaurantId}
// RestaurantsPage -> restaurantform (create/edit)
// DailyMenuPage -> dailymenuform?restaurantId={id}&menuId={menuId}