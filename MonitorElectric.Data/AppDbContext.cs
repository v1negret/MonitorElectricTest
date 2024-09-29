using Microsoft.EntityFrameworkCore;
using MonitorElectric.Models;
using MonitorElectric.Models.Entities;

namespace MonitorElectric.Data;

public class AppDbContext : DbContext
{
    public DbSet<RssItemEntity> RssItems { get; set; }
    private string _connectionString;
    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}