using MonitorElectric.Models;

namespace MonitorElectric.Services.Interfaces;

public interface IExcelService
{
    public Task WriteAsOpenXml(string path, List<RssItem> items);
}