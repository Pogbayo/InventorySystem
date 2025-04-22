using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InventorySystem.Infrastructure.Persistence
{
    public class InventorySystemDbFactory : IDesignTimeDbContextFactory<InventorySystemDb>
    {
        public InventorySystemDb CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            //Console.WriteLine("Connection string: " + connectionString); 

            var optionsBuilder = new DbContextOptionsBuilder<InventorySystemDb>();
            optionsBuilder.UseSqlServer(connectionString);

            return new InventorySystemDb(optionsBuilder.Options);
        }
    }
}
