using MonitorElectric.Models;
using MonitorElectric.Models.Entities;

namespace MonitorElectric.Services.Interfaces;

public interface IRssItemsService
{
    public Task<bool> Add(RssItemEntity item);
    public Task<bool> Add(List<RssItemEntity> items);
    public Task<bool> Remove(RssItemEntity item);
    public Task<bool> Update(RssItemEntity item);
    public Task<RssItemEntity> GetById(string id);
    public Task<List<RssItemEntity>> GetByTitle(string title);
    public Task<List<RssItemEntity>> GetByAuthor(string author);
    public Task<List<RssItemEntity>> GetByAuthorAndTitle(string author, string title);
    public Task<List<RssItemEntity>> FindByWord(string word);
    public Task<List<RssItemEntity>> GetAll();
}