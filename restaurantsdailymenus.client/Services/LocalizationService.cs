using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace restaurantsdailymenus.client.Services;

public static class LocalizationService
{
    public static void SetLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);

        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        // Recria a UI
        Application.Current.MainPage = new AppShell();
    }
}