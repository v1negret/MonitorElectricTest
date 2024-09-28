

using MonitorElectric.Models;
using MonitorElectric.Services.Interfaces;
using OfficeOpenXml;

namespace MonitorElectric.Services;

public class ExcelService : IExcelService
{
    public async Task WriteAsOpenXml(string path, List<RssItem> items)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var excelPackage = new ExcelPackage())
        {
            var worksheet = excelPackage.Workbook.Worksheets.Add("RssExcel");
            worksheet.Cells[1,1].Value = "Заголовок";
            worksheet.Cells[1, 2].Value = "Ссылка";
            worksheet.Cells[1, 3].Value = "Дата публикации";
            worksheet.Cells[1, 4].Value = "Автор";
            worksheet.Cells[1, 5].Value = "Описание";

            for (int i = 0; i < items.Count; i++)
            {
                worksheet.Cells[i+2,1].Value = items[i].Title;
                worksheet.Cells[i+2,2].Value = items[i].Link;
                worksheet.Cells[i+2,3].Value = items[i].PublishDate.ToString("dd-MM-yyyy HH:mm:ss");
                worksheet.Cells[i+2,4].Value = items[i].Author;
                worksheet.Cells[i+2,5].Value = items[i].Description;
            }
            
            await excelPackage.SaveAsAsync(new FileInfo(path));
        }
    }
}