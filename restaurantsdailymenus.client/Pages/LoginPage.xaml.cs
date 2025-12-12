using restaurantsdailymenus.client.Models;
using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace restaurantsdailymenus.client.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
