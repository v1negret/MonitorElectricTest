namespace MonitorElectric.Models.Entities;

public class RssItemEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public DateTime PublishDate { get; set; }
    public string? Author { get; set; }
    public string Description { get; set; }
}