using MonitorElectric.Models;
using MonitorElectric.Services;
using OfficeOpenXml;

namespace MonitorElectric.Tests;

public class ExcelServiceTest
{
    [Fact]
    public async Task ExcelService_WriteAsOpenXml_ShouldCreateExcelFile()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFile.xlsx");
        var items = new List<RssItem>()
        {
            new RssItem()
            {
                Author = "John Doe", Description = "Description 1", Link = "https://", PublishDate = DateTime.Now,
                Title = "Title 1"
            },
            new RssItem()
            {
                Author = "Doe John", Description = "Description 2", Link = "https://", PublishDate = DateTime.Now,
                Title = "Title 2"
            },
        };
        var excelService = new ExcelService();
        await excelService.WriteAsOpenXml(path, items);
        
        Assert.True(File.Exists(path));
        File.Delete(path);
    }
}