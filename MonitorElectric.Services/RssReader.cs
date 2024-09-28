using MonitorElectric.Models;
using MonitorElectric.Services.Interfaces;

namespace MonitorElectric.Services;

public class RssReader : IRssReader
{
    public async Task<string> Read(string url)
    {
        using (var client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }
}