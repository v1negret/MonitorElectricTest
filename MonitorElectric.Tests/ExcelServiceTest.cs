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
    
    
    //Данный тест выглядит не очень красиво и правильно, но я подумал, что он необходим
    [Fact]
    public async Task ExcelService_WriteAsOpenXml_ShouldContainsCorrectContent()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFile.xlsx");
        var items = new List<RssItem>()
        {
            new RssItem()
            {
                Author = "John Doe", Description = "Description 1", Link = "https://", PublishDate = DateTime.Now,
                Title = "Title 1"
            }
        };
        var excelService = new ExcelService();
        await excelService.WriteAsOpenXml(path, items);
        
        var excelPackage = new ExcelPackage(new FileInfo(path));
        var worksheet = excelPackage.Workbook.Worksheets["RssExcel"];
        
        Assert.Equal(worksheet.Cells[1,1].Value, "Заголовок");
        Assert.Equal(worksheet.Cells[1,2].Value, "Ссылка");
        Assert.Equal(worksheet.Cells[1,3].Value, "Дата публикации");
        Assert.Equal(worksheet.Cells[1,4].Value, "Автор");
        Assert.Equal(worksheet.Cells[1,5].Value, "Описание");
        Assert.Equal(worksheet.Cells[2,1].Value, "Title 1");
        Assert.Equal(worksheet.Cells[2,2].Value, "https://");
        Assert.Equal(worksheet.Cells[2,4].Value, "John Doe");
        Assert.Equal(worksheet.Cells[2,5].Value, "Description 1");
        
        worksheet.Dispose();
        excelPackage.Dispose();
        
        File.Delete(path);
    }
}