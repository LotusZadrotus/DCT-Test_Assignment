using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CryptoInfo.ViewModels;

namespace CryptoInfo.Services;

public static class PageResolver
{
    public static Dictionary<string, Func<Page>> Pages { get; } = new()
    {
        {
            "main", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.Main;
            }
        },
        {
            "search", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.Search;
            }
        },{
            "asset", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.Asset;
            }
        }
    };
    public static Dictionary<string, Func<IViewModel>> ViewModels { get; } = new()
    {
        {
            "main", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.MainViewModel;
            }
        },
        {
            "search", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.SearchViewModel;
            }
        },
        {
            "asset", () =>
            {
                var data = App.Current.MainWindow.DataContext as MainWindowViewModel;
                return data.AssetViewModel;
            }
        }
    };
}