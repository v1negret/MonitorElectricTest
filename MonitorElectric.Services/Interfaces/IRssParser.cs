using MonitorElectric.Models;

namespace MonitorElectric.Services.Interfaces;

public interface IRssParser
{
    public List<RssItem> Parse(string xmlString);
}