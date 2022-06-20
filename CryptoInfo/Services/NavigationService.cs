using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CryptoInfo.Pages;
using CryptoInfo.ViewModels;

namespace CryptoInfo.Services;

public class NavigationService
{
    private System.Windows.Navigation.NavigationService _service;
    private static NavigationService _instance;
    private static readonly object sync = new();
    
    private static NavigationService Instanse
    {
        get
        {
            if (_instance == null)
            {
                lock (sync)
                {
                    _instance ??= new NavigationService();
                }
            }
            return _instance;
        }
    }
    


    private NavigationService()
    {

    }
    private void Navigated(object sender, NavigationEventArgs e)
    {
        if (e.Content is not Page page) return;
        page.DataContext = e.ExtraData;
    }

    public static void Navigate(Page page, IViewModel viewModel)
    {
        _instance._service.Navigate(page, viewModel);
    }
    public static void Navigate(string name)
    {
        Navigate(PageResolver.Pages[name].Invoke(), PageResolver.ViewModels[name].Invoke());
    }

    public static void NavigateToAsset(string id)
    {
        var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
        var assetViewModel = data.AssetViewModel as AssetViewModel;
        assetViewModel.GetAsset(id);
        Navigate("asset");
    }
    public static System.Windows.Navigation.NavigationService Service
    {
        get => _instance._service;
        set
        {
            if (Instanse._service is not null)
            {
                _instance._service.Navigated -= Instanse.Navigated;
            }

            Instanse._service = value;
            Instanse._service.Navigated += Instanse.Navigated;
        }
    }
    
}