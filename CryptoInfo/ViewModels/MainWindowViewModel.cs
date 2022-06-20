using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoInfo.Pages;
using CryptoInfo.Services;

namespace CryptoInfo.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private const string MainPage = "main";
    private const string SearchPage = "search";

    private string _lastPage = "";
    public ICommand ToSearchPage { get; set; }
    public ICommand ToMainPage { get; set; }

    public ICommand Refresh
    {
        get;
        set;
    }
    public ICommand SelectAsset { get; }

    private void GoToMainPage(INotifyPropertyChanged viewModel)
    {
        NavigationService.Navigate(MainPage);
    }

    public Page Main { get; }
    public IViewModel MainViewModel { get; }
    public Page Search { get; }
    public IViewModel SearchViewModel { get; }
    
    public Page Asset { get; }
    
    public IViewModel AssetViewModel { get; }

    public MainWindowViewModel()
    {
        Refresh = new RelayCommand(x => NavigationService.Service.Refresh());
        Main = new MainPage();
        Search = new SearchPage();
        Asset = new AssetPage();
        MainViewModel = new MainPageViewModel();
        SearchViewModel = new SearchViewModel();
        AssetViewModel = new AssetViewModel();
        ToSearchPage = new RelayCommand(x=> NavigationService.Navigate(Search, SearchViewModel));
        ToMainPage = new RelayCommand(x => { NavigationService.Navigate(Main, MainViewModel); });
        SelectAsset = new RelayCommand(x => MessageBox.Show("32"));
    }
    
    
    
}