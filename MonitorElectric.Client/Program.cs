using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using MonitorElectric.Data;
using MonitorElectric.Services;
using MonitorElectric.Services.Interfaces;
using Serilog;

using var log = new LoggerConfiguration()
    .WriteTo
    .Console()
    .WriteTo
    .File(Path.Combine("logs", "log.txt"), rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:dd-MM-yyy HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();

IRssReader rssReader = new RssReader();
IRssParser rssParser = new RssParser();
IExcelService excelService = new ExcelService();

try
{
    string configLocation = Path.Combine(Directory.GetCurrentDirectory(), "config.txt");
    string href = "";

    if (args.Length != 0)
    {
        href = args[0];
    }
    else
    {
        if (File.Exists(configLocation))
        {
            href = File.ReadAllText(configLocation);
        }
        else
        {
            Console.WriteLine("""
                              Необходимо указать адрес RSS-ленты. Сделать это можно:
                                а) Указав в качестве параметра в консоли. Пример: 'MonitorElectric.Client "https://habr.com/ru/rss/articles/"'
                                б) Указав адрес ленты в конфигурационном файле config.txt
                                
                              Нажмите любую клавишу чтобы выйти...
                              """);
            Console.ReadKey();
            return;
        }

    }

    log.Information($"Начало чтения RSS");
    var text = await rssReader.Read(href);
    log.Information($"Конец чтения RSS");

    log.Information($"Начало конвертации RSS");
    var items = rssParser.Parse(text);
    log.Information($"Конец конвертации RSS");

    log.Information($"Начало конвертации RSS");
    await excelService.WriteAsOpenXml(
        Path.Combine(Directory.GetCurrentDirectory(),
            $"Выгрузка {DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss", CultureInfo.InvariantCulture)}.xlsx"), items);
    log.Information($"Конец конвертации RSS");
}
catch (HttpRequestException ex)
{
    log.Error("Введён некорректный адрес RSS-ленты, либо отсутствует подключение к интернету.");

    Console.WriteLine("Нажмите любую кнопку чтобы выйти...");
    Console.ReadKey();
}
catch (XmlException ex)
{
    log.Error("На указанной странице не была обнаружена RSS-лента");

    Console.WriteLine("Нажмите любую кнопку чтобы выйти...");
    Console.ReadKey();
}
