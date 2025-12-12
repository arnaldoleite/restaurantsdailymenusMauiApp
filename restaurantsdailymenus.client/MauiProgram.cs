using Microsoft.Extensions.Logging;
using RestaurantsDailyMenus.Api;
using System.Net.Http.Headers;

namespace restaurantsdailymenus.client;



public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
         
        // JWT Token Service
        builder.Services.AddSingleton<TokenService>();

        // Add HttpClient + token handler
        builder.Services.AddTransient<TokenHandler>();

        builder.Services.AddHttpClient<AuthClient>(client =>
        {
            client.BaseAddress = new Uri("https://your-api-url/");
        });

        builder.Services.AddHttpClient<RestaurantsClient>(client =>
        {
            client.BaseAddress = new Uri("https://your-api-url/");
        })
        .AddHttpMessageHandler<TokenHandler>();

        builder.Services.AddHttpClient<DailyMenusClient>(client =>
        {
            client.BaseAddress = new Uri("https://your-api-url/");
        })
        .AddHttpMessageHandler<TokenHandler>();

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


