using Microsoft.EntityFrameworkCore;
using MonitorElectric.Models;
using MonitorElectric.Models.Entities;

namespace MonitorElectric.Data;

public class AppDbContext : DbContext
{
    public DbSet<RssItemEntity> RssItems { get; set; }
    private string ConnectionString;
    public AppDbContext(string connectionString)
    {
        ConnectionString = connectionString;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}