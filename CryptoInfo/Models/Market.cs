using Newtonsoft.Json;

namespace CryptoInfo.Models;

public class Market
{
    [JsonProperty("exchangeId")]
    public string ExchangeId { get; set; }
    [JsonProperty("rank")]
    public short Rank { get; set; }
    [JsonProperty("QuoteSymbol")]
    public string QuoteSymbol { get; set; }
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; }
    [JsonProperty("priceQuote")]
    public decimal PriceQuote { get; set; }
    [JsonProperty("priceUsd")]
    public decimal? PriceUsd { get; set; }
    [JsonProperty("percentExchangeVolume")]
    private float? PercentExchangeVolume { get; set; }
    
}