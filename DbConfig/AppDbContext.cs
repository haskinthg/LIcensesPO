
using System.IO;
using LIcensesPO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LIcensesPO.DbConfig;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        Database.Migrate();
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; } = null;
    public DbSet<Prog> Progs { get; set; } = null;
    public DbSet<Computer> Computers { get; set; } = null;
    public DbSet<License> Licenses { get; set; } = null;
    public DbSet<Licensor> Licensors { get; set; } = null;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(("appsetting.json"));
            var config = builder.Build();

            options.EnableServiceProviderCaching();
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), opt => opt.EnableRetryOnFailure());
            base.OnConfiguring(options);
        }
    }
}