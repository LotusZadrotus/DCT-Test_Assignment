using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CryptoInfo.Models;
using CryptoInfo.ViewModels;
using Newtonsoft.Json.Linq;

namespace CryptoInfo.Services;

public class WebSocketService: IDisposable
{
    private const string API_URL = @"wss://ws.coincap.io/prices?assets=";
    private string _assets = "";
    private ClientWebSocket _socket;
    private readonly object _sync = new();

    public delegate void Update(JObject updates);

    public event Update OnUpdate;
    public WebSocketService(List<Asset> assets)
    {
        foreach (var item in assets)
        { 
            
            _assets = $"{_assets}{item.Id},";
        }

        _assets=_assets.TrimEnd(',');
        _socket = new ClientWebSocket();
    }
    

    public async void Connect()
    {
        await _socket.ConnectAsync(new Uri(API_URL + _assets), CancellationToken.None);
        // MessageBox.Show(_assets);
        await Echo();
    }

    private async Task Echo()
    {
        var buffer = new byte[1024 * 4];
        var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        while (!result.CloseStatus.HasValue)
        {
            await Task.Run(() =>
            {
                var json = JObject.Parse(Encoding.UTF8.GetString(buffer));
                OnUpdate.Invoke(json);
                buffer = new byte[1024 * 4];
            });
            result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }
    }

    public void Close()
    {
        _socket.CloseAsync(WebSocketCloseStatus.NormalClosure,"",CancellationToken.None);
    }

    public void Dispose()
    {
        _socket.Dispose();
        Close();
    }
}