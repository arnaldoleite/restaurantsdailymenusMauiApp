// ================================================
// MauiProgram.cs
// 
// ================================================

using Microsoft.Extensions.Logging;
using restaurantsdailymenus.client.Models;
using restaurantsdailymenus.client.Pages;
using RestaurantsDailyMenus.Api;
using System.Net.Http.Headers;
using System.Globalization;

namespace restaurantsdailymenus.client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var culture = CultureInfo.CurrentUICulture; //local culture
        CultureInfo.DefaultThreadCurrentCulture = culture; // set for dates, numbers, etc.
        CultureInfo.DefaultThreadCurrentUICulture = culture; // set for strings

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // =====================================
        // TOKEN + HANDLERS
        // =====================================
        builder.Services.AddSingleton<TokenService>();
        builder.Services.AddTransient<TokenHandler>();

        //var apiBaseUrl = "http://127.0.0.1:5102/"; // <-- change this

        var apiBaseUrl = "http://10.0.2.2:5102/";

        // =====================================
        // HTTP CLIENTS (API)
        // =====================================
        builder.Services.AddHttpClient<AuthClient>(c =>
        {
            c.BaseAddress = new Uri(apiBaseUrl);
        });


        builder.Services.AddHttpClient<RestaurantsClient>(c =>
        {
            c.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<TokenHandler>();

        builder.Services.AddHttpClient<DailyMenusClient>(c =>
        {
            c.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<TokenHandler>();

        builder.Services.AddHttpClient<TestClient>(c =>
        {
            c.BaseAddress = new Uri(apiBaseUrl);
        }).AddHttpMessageHandler<TokenHandler>();

        // =====================================
        // VIEWMODELS
        // =====================================
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RestaurantViewModel>();
        builder.Services.AddTransient<RestaurantsViewModel>();
        builder.Services.AddTransient<RestaurantDetailsViewModel>();
        builder.Services.AddTransient<DailyMenuViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();


        // =====================================
        // PAGES
        // =====================================
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RestaurantsPage>();
        builder.Services.AddTransient<RestaurantPage>();
        builder.Services.AddTransient<RestaurantDetailsPage>();
        builder.Services.AddTransient<DailyMenuPage>();
        builder.Services.AddTransient<RegisterPage>();
        


        // =====================================
        // SHELL
        // =====================================
        builder.Services.AddSingleton<AppShell>();

        return builder.Build();
    }
}


public class TokenHandler : DelegatingHandler
{
    private readonly TokenService _tokenService;

    public TokenHandler(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
public class TokenService
{
    const string TokenKey = "jwt_token";

    public Task SaveTokenAsync(string token) =>
        SecureStorage.SetAsync(TokenKey, token);

    public Task<string> GetTokenAsync() =>
        SecureStorage.GetAsync(TokenKey);

    public void Logout() =>
        SecureStorage.Remove(TokenKey);
}


