using MonitorElectric.Models;

namespace MonitorElectric.Services.Interfaces;

public interface IRssReader
{
    public Task<string> Read(string url);
}