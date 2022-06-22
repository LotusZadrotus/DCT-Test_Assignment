using System.ComponentModel;
using System.Runtime.CompilerServices;
using CryptoInfo.Annotations;

namespace CryptoInfo.Models;

public class Asset: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    // private string _id ="";
    private int _rank;
    // private string _symbol ="";
    // private string name = "";
    public decimal? Supply { get; set; }
    public decimal? MaxSupply { get; set; }
    public decimal? MarketCapUsd{ get; set; }
    public decimal? VolumeUsd24Hr{ get; set; }
    private decimal _priceUsd;
    public float? ChangePercent24Hr { get; set; }
    public float? Vwap24Hr { get; set; }
    public string Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public int Rank
    {
        get => _rank;
        set
        {
            _rank = value;
            OnPropertyChanged(nameof(Rank));
        }
    }

    public decimal PriceUsd
    {
        get => _priceUsd;
        set
        {
            _priceUsd = value;
            OnPropertyChanged(nameof(PriceUsd));
        }
    }

    public Asset()
    {
        
    }
    public Asset(string id, string name, string symbol, int rank, decimal priceUsd)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
        Rank = rank;
        PriceUsd = priceUsd;
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}