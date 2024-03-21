using HelloMaui1.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloMaui1.Services
{
    public class AladdinContext : DbContext
    {
        public DbSet<Wish> Wishes { get; set; }

        public AladdinContext()
        {
            //SQLitePCL.Batteries_V2.Init();//For IOS
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "aladdin.sqlite");
            options.UseSqlite($"Filename={dbPath}");
        }

    }
}
