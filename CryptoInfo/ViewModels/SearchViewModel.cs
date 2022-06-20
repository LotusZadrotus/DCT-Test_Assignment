using System.Collections.ObjectModel;
using System.Windows.Input;
using CryptoInfo.Models;
using CryptoInfo.Services;

namespace CryptoInfo.ViewModels;

public class SearchViewModel : BaseViewModel
{
    private ObservableCollection<Asset> _assets= new();
    private readonly AssetsService _service;
    public ObservableCollection<Asset> Assets
    {
        get => _assets;
        set
        {
            _assets = value;
            OnPropertyChanged(nameof(Assets));
        } 
    }
    
    public ICommand Search { get; set; }
    public SearchViewModel()
    {
        _service = new AssetsService();
        Search = new RelayCommand(SearchMethod);
    }

    private void SearchMethod(object name)
    {
        Assets = _service.GetAssets(name as string);
    }


    
    public override void Refresh()
    {
        base.Refresh();
    }
    
}