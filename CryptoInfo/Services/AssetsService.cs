using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CryptoInfo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CryptoInfo.Services;

public class AssetsService
{
    private const string API_URL = @"https://api.coincap.io/v2/assets";
    private readonly HttpClient _httpClient;
    private const int Limit = 10;

    public Asset GetAssetById(string id)
    {
        var response = _httpClient.GetAsync(API_URL + $"/{id}").Result;
        if (response.IsSuccessStatusCode)
        {
            var result =  JObject.Parse(response.Content.ReadAsStringAsync().Result);
        
            return result["data"].ToObject<Asset>();
        }
        throw new HttpRequestException(response.StatusCode.ToString());
    }

    public ObservableCollection<Asset> GetAssets()
    {
        var response = _httpClient.GetAsync(API_URL + $"?limit={Limit}").Result;
        if (response.IsSuccessStatusCode)
        {
            var result =  JObject.Parse(response.Content.ReadAsStringAsync().Result);
        
            return result["data"].ToObject<ObservableCollection<Asset>>();
        }
        throw new HttpRequestException(response.StatusCode.ToString());
    }

    public ObservableCollection<Asset> GetAssets(string? name)
    {
        var response = _httpClient.GetAsync(API_URL + $"?search={name}").Result.Content.ReadAsStringAsync();
        var result =  JObject.Parse(response.Result);

        return result["data"]?.ToObject<ObservableCollection<Asset>>() ?? new ObservableCollection<Asset>();
    }

    public AssetsService()
    {
        _httpClient = new HttpClient();
    }


}