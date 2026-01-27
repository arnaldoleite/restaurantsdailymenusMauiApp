using restaurantsdailymenus.client;
using restaurantsdailymenus.client.Models;
using RestaurantsDailyMenus.Api;
using restaurantsdailymenus.client.Services;    
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace restaurantsdailymenus.client.Models;
// ==================================
// LOGIN VIEWMODEL
// ==================================
public class LoginViewModel : BaseViewModel
{
    private readonly AuthClient _auth;
    private readonly TokenService _tokenService;

    public string Username { get; set; }
    public string Password { get; set; }

    public ICommand LoginCommand { get; }
    public ICommand GoToRegisterCommand { get; }
    public ICommand ChangeLanguageCommand { get; }
    public LoginViewModel(AuthClient auth, TokenService tokenService)
    {
        _auth = auth;
        _tokenService = tokenService;

        LoginCommand = new Command(async () => await LoginAsync());
        GoToRegisterCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("register");
        });
        ChangeLanguageCommand = new Command<string>(ChangeLanguage);
    }
    void ChangeLanguage(string language)
    {
        // "pt" ou "en"
        LocalizationService.SetLanguage(language);
    }
    private async Task LoginAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var response = await _auth.LoginAsync(new LoginDto
            {
                Username = Username,
                Password = Password
            });

            if (response?.Token != null)
            {
                await _tokenService.SaveTokenAsync(response.Token);
                await Shell.Current.GoToAsync("//restaurants");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlertAsync("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}