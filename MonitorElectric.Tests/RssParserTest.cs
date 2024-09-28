using System.Xml;
using MonitorElectric.Models;
using MonitorElectric.Services;
using MonitorElectric.Services.Interfaces;

namespace MonitorElectric.Tests;

public class RssParserTest
{
    [Theory]
    [InlineData("<item><guid>https://lenta.ru/news/2024/09/27/tarasova-otsenila-vozmozhnoe-vozvraschenie-rossiyskih-figuristov-na-mezhdunarodnye-turniry/</guid><author>Дарья Коршунова</author><title>Тарасова оценила возможное возвращение российских фигуристов на международные турниры</title><link>https://lenta.ru/news/2024/09/27/tarasova-otsenila-vozmozhnoe-vozvraschenie-rossiyskih-figuristov-na-mezhdunarodnye-turniry/</link><description><![CDATA[]]></description><pubDate>Fri, 27 Sep 2024 19:22:18 +0300</pubDate><enclosure url=\"https://icdn.lenta.ru/images/2024/09/27/14/20240927145453751/pic_4fc550d4842d501e35f1f36b7bea970a.jpg\" type=\"image/jpeg\" length=\"24450\"/><category>Спорт</category></item>")]
    public async Task RssParser_Parse_ShouldReturnCorrectObject(string xmlString)
    {
        IRssParser rssParser = new RssParser();
        var expectedResult = new RssItem()
        {
            Title = "Тарасова оценила возможное возвращение российских фигуристов на международные турниры",
            Author = "Дарья Коршунова",
            Link =
                "https://lenta.ru/news/2024/09/27/tarasova-otsenila-vozmozhnoe-vozvraschenie-rossiyskih-figuristov-na-mezhdunarodnye-turniry/",
            PublishDate = DateTime.Parse("Fri, 27 Sep 2024 19:22:18 +0300"),
        };
        
        var result = rssParser.Parse(xmlString).FirstOrDefault();
        Assert.Equal(expectedResult.Title, result.Title);
        Assert.Equal(expectedResult.Author, result.Author);
        Assert.Equal(expectedResult.Link, result.Link);
        Assert.Equal(expectedResult.PublishDate, result.PublishDate);
    }
    
    [Theory]
    [InlineData("https://youtube.com/")]
    public async Task RssParser_Parse_ShouldThrowXmlException(string url)
    {
        IRssReader rssReader = new RssReader();
        IRssParser rssParser = new RssParser();
        
        var text = await rssReader.Read(url);
        Func<List<RssItem>> result = () => rssParser.Parse(text);

        Assert.Throws<XmlException>(result);
    }
}