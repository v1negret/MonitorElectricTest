using Microsoft.EntityFrameworkCore;
using MonitorElectric.Data;
using MonitorElectric.Models.Entities;
using MonitorElectric.Services.Interfaces;

namespace MonitorElectric.Services;

public class RssItemsService : IRssItemsService
{
    private readonly AppDbContext _db;

    public RssItemsService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<bool> Add(RssItemEntity item)
    {
        if (item is null) return false;
        await _db.RssItems.AddAsync(item);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Add(List<RssItemEntity> items)
    {
        if(items.Count == 0) return false;
        foreach (var item in items)
        {
            await _db.RssItems.AddAsync(item);
        }
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Remove(RssItemEntity item)
    {
        if (item is null) return false;
        _db.Remove(item);
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Update(RssItemEntity item)
    {
        if(item is null) return false;
        _db.Update(item);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<RssItemEntity> GetById(string id)
    {
        if(String.IsNullOrEmpty(id)) return null;
        
        return await _db.RssItems.FindAsync(id);
    }

    public async Task<List<RssItemEntity>> GetByTitle(string title)
    {
        if (string.IsNullOrEmpty(title)) return null;
        return await _db.RssItems.Where(x => x.Title.Contains(title)).ToListAsync();
    }

    public async Task<List<RssItemEntity>> GetByAuthor(string author)
    {
        if (string.IsNullOrEmpty(author)) return null;
        return await _db.RssItems.Where(x => x.Author.Contains(author)).ToListAsync();
    }

    public async Task<List<RssItemEntity>> GetByAuthorAndTitle(string author, string title)
    {
        if(string.IsNullOrEmpty(author) || string.IsNullOrEmpty(title)) return null;
        return await _db.RssItems.Where(x => x.Author.Contains(author) && x.Title.Contains(title)).ToListAsync();
    }

    public async Task<List<RssItemEntity>> FindByWord(string word)
    {
        if(string.IsNullOrEmpty(word)) return null;
        return await _db.RssItems.Where(item =>
            item.Title.Contains(word) ||
            item.Description.Contains(word) ||
            item.Author.Contains(word))
            .ToListAsync();
    }

    public async Task<List<RssItemEntity>> GetAll()
    {
        return await _db.RssItems.ToListAsync();
    }
}