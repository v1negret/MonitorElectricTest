using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

try
{

    if (!File.Exists("appsettings.json"))
    {
        using (var sw = File.CreateText("appsettings.json"))
        {
            sw.WriteLine(
                "{\n  \"ConnectionStrings\": {\n    \"DbConnectionString\": \"\",\n    \"RssConnectionString\": \"\"\n  }\n}");
        }
    }

    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddCommandLine(args)
        .Build();


    var dbConnectionString = configuration["ConnectionStrings:DbConnectionString"];
    var rssConnectionString = configuration["ConnectionStrings:RssConnectionString"];

    IRssReader rssReader = new RssReader();
    IRssParser rssParser = new RssParser();
    IExcelService excelService = new ExcelService();

    var href = configuration["h"];
    if(string.IsNullOrEmpty(href))
    {
        if (!string.IsNullOrEmpty(rssConnectionString))
        {
            href = rssConnectionString;
        }
        else
        {
            Console.WriteLine("""
                              Необходимо указать адрес RSS-ленты. Сделать это можно:
                                а) Указав в параметре --h в консоли. Пример: 'MonitorElectric.Client --h "https://habr.com/ru/rss/articles/"'
                                б) Указав адрес ленты в конфигурационном файле appsettings.json
                                
                              Нажмите любую клавишу чтобы выйти...
                              """);
            Console.ReadKey();
            return;
        }

    }

    log.Information($"Начало чтения RSS из {href}");
    var text = await rssReader.Read(href);
    log.Information($"Конец чтения RSS {href}");

    log.Information($"Начало конвертации RSS");
    var items = rssParser.Parse(text);
    log.Information($"Конец конвертации RSS");

    log.Information($"Начало конвертации RSS");
    await excelService.WriteAsOpenXml(
        Path.Combine(Directory.GetCurrentDirectory(),
            $"Выгрузка {DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss", CultureInfo.InvariantCulture)}.xlsx"), items);
    log.Information($"Конец конвертации RSS");

    Console.WriteLine("Нажмите любую кнопку чтобы выйти...");
    Console.ReadKey();
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
catch (InvalidDataException ex)
{
    log.Error("Ошибка при чтении конфигурационного файла appsettings.json");
    
    Console.WriteLine("Нажмите любую кнопку чтобы выйти...");
    Console.ReadKey();
}
