
using restaurantsdailymenus.client.Models;
using restaurantsdailymenus.client.Resources.Localization;
using RestaurantsDailyMenus.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace restaurantsdailymenus.client.Models;

public class RegisterViewModel : BaseViewModel
{
    readonly AuthClient _authClient;


    string _username;
    string _email;
    string _password;
    string _confirmPassword;


    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }


    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }


    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }


    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
    }


    public ICommand RegisterCommand { get; }
    public ICommand GoToLoginCommand { get; }


    public RegisterViewModel(AuthClient authClient)
    {
        _authClient = authClient;


        RegisterCommand = new Command(async () => await RegisterAsync());
        GoToLoginCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }


    async Task RegisterAsync()
    {
        if (IsBusy)
            return;


        if (Password != ConfirmPassword)
        {
            await Application.Current.MainPage.DisplayAlertAsync(
            AppResources.error,
            AppResources.passwords_do_not_match,
            "OK");
            return;
        }


        IsBusy = true;


        try
        {
            await _authClient.RegisterAsync(new RegisterDto
            {
                Username = Username,
                //Email = Email,
                Password = Password
            });


            await Application.Current.MainPage.DisplayAlertAsync(
            AppResources.success,
            AppResources.account_created_successfully,
            "OK");


            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlertAsync(
            AppResources.error,
            ex.Message,
            "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}