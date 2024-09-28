using MonitorElectric.Models;
using MonitorElectric.Services;
using MonitorElectric.Services.Interfaces;

namespace MonitorElectric.Tests;

public class RssReaderTest
{
    [Theory]
    [InlineData("https://habr.com/ru/")]
    public async Task RssReader_Read_ShouldNotReturnEmptyString(string url)
    {
        IRssReader rssReader = new RssReader();
        
        var result = await rssReader.Read(url);
        
        Assert.False(String.IsNullOrEmpty(result));
    }
    
    [Theory]
    [InlineData("https://habr.co/")]
    public async Task RssReader_Read_ShouldThrowHttpRequestException(string url)
    {
        IRssReader rssReader = new RssReader();
        
        Func<Task> result = async () => await rssReader.Read(url);

        await Assert.ThrowsAsync<HttpRequestException>(result);
    }
}