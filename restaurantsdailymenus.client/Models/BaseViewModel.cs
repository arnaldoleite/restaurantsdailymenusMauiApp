using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RestaurantsDailyMenus.Api;

namespace restaurantsdailymenus.client.Models;
// ==================================
// BASE VIEWMODEL
// ==================================
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    //protected void OnPropertyChanged(string name)
    //   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    bool _isBusy;
    string _title;
    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
    }
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }
    //public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(
        ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged(
        [CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}