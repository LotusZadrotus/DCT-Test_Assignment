using System.Collections.Generic;
using System.Net.Http;
using CryptoInfo.Models;
using Newtonsoft.Json.Linq;

namespace CryptoInfo.Services;

public class MarketsService
{
    private const string API_URL = @"https://api.coincap.io/v2/markets";
    private readonly HttpClient _httpClient;

    public MarketsService()
    {
        _httpClient = new HttpClient();
    }

    public List<Market> GetMarketsById(string id)
    {
        var response = _httpClient.GetAsync(API_URL + $"?baseId={id}").Result;
        if (response.IsSuccessStatusCode)
        {
            var result =  JObject.Parse(response.Content.ReadAsStringAsync().Result);
        
            return result["data"].ToObject<List<Market>>();
        }

        throw new HttpRequestException(response.StatusCode.ToString());
    }
}