using System;
using System.Collections.Generic;
using System.Text;

namespace restaurantsdailymenus.client.Pages
{
    public partial class RestaurantDetailsPage : ContentPage
    {
        public RestaurantDetailsPage(RestaurantDetailsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
