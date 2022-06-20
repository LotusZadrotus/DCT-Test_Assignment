using System.Windows.Controls;
using System.Windows.Input;
using CryptoInfo.Models;
using CryptoInfo.ViewModels;

namespace CryptoInfo.Pages;

public partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
    {
        var view = sender as ListView;
        var asset = view.SelectedItem as Asset;
        Services.NavigationService.NavigateToAsset(asset.Id);
    }
}