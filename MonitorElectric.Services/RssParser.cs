using System.Runtime.InteropServices;
using System.Xml;
using MonitorElectric.Models;
using MonitorElectric.Services.Interfaces;
using ReadSharp;

namespace MonitorElectric.Services;

public class RssParser : IRssParser
{
    public List<RssItem> Parse(string xmlString)
    {
        if (string.IsNullOrEmpty(xmlString))
        {
            return null;
        }
        var rssItems = new List<RssItem>();
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlString);

        foreach (XmlNode item in xmlDoc.SelectNodes("//item"))
        {
            RssItem rssItem = new RssItem
            {
                Title = item["title"].InnerText,
                Link = item["link"].InnerText,
                PublishDate = DateTime.Parse(item["pubDate"].InnerText),
                Author = item["author"]?.InnerText,
                Description = HtmlUtilities.ConvertToPlainText(item["description"].InnerText),
            };
            rssItems.Add(rssItem);
        }

        return rssItems;
    }
}