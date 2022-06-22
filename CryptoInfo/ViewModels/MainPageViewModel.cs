using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using CryptoInfo.Annotations;
using CryptoInfo.Models;
using CryptoInfo.Services;

namespace CryptoInfo.ViewModels;

public class MainPageViewModel: BaseViewModel
{
    private ObservableCollection<Asset> _assets;
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
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
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