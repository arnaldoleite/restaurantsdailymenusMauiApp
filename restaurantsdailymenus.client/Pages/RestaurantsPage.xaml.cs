using restaurantsdailymenus.client.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace restaurantsdailymenus.client.Pages
{
    public partial class RestaurantsPage : ContentPage
    {
        public RestaurantsPage(RestaurantsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
