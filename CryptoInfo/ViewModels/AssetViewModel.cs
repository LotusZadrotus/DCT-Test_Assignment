using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using CryptoInfo.Models;
using CryptoInfo.Services;

namespace CryptoInfo.ViewModels;

public class AssetViewModel: BaseViewModel
{
    private Asset _asset;
    private readonly AssetsService _assetsService;
    private readonly MarketsService _marketsService;
    private List<Market> _markets;
    private string _id;
    public Asset Asset
    {
        get => _asset;
        set
        {
            _asset = value;
            OnPropertyChanged(nameof(Asset));
        }
    }

    public List<Market> Markets
    {
        get => _markets;
        set
        {
            _markets = value;
            OnPropertyChanged(nameof(Markets));
        }
    }

    public AssetViewModel()
    {
        _assetsService = new AssetsService();
        _marketsService = new MarketsService();
    }

    public void GetAsset(string id)
    {
        try
        {
            _id = id;
            Asset = _assetsService.GetAssetById(id);
            Markets = _marketsService.GetMarketsById(id);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public override void Refresh()
    {
        GetAsset(_id);
    }
}