using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using CryptoInfo.Annotations;
using CryptoInfo.Models;
using CryptoInfo.Services;
using Newtonsoft.Json.Linq;

namespace CryptoInfo.ViewModels;

public class MainPageViewModel: BaseViewModel
{
    private ObservableCollection<Asset> _assets;
    private WebSocketService _webSocket;
    public ObservableCollection<Asset> Assets
    {
        get => _assets;
        set
        {
            _assets = value;
            OnPropertyChanged(nameof(Assets));
        }
    }

    private AssetsService _service;
    public MainPageViewModel()
    {
        try
        {
            _service = new AssetsService();
            _assets = _service.GetAssets();
            _webSocket = new WebSocketService(_assets.ToList());
            _webSocket.Connect();
            _webSocket.OnUpdate += UpdateHandler;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void UpdateHandler(JObject items)
    {
        foreach (var item in items)
        {
            var data = _assets.FirstOrDefault(x => x.Id == item.Key);
            if (data is not null) data.PriceUsd = Convert.ToDecimal(item.Value);
        }
    }
    public override void Refresh()
    {
        try
        {
            Assets = _service.GetAssets();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}