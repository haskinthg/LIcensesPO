using System.IO;
using LIcensesPO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LIcensesPO.DbConfig;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Program> Programs { get; set; }
    public DbSet<Computer> Computers { get; set; }
    public DbSet<License> Licenses { get; set; }
    public DbSet<Licensor> Licensors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("(localdb)\\mssqllocaldb;Database=LicensesPO;User Id=myUsername;Password=myPassword;");
    }
}