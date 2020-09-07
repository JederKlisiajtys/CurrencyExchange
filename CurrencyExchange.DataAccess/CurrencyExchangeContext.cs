using CurrencyExchange.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CurrencyExchange.DataAccess
{
    public class CurrencyExchangeContext : DbContext
    {
        public CurrencyExchangeContext(DbContextOptions<CurrencyExchangeContext> options)
            :base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CurrencyExchangeContext>
    {
        public CurrencyExchangeContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../CurrencyExchange/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<CurrencyExchangeContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);
            return new CurrencyExchangeContext(builder.Options);
        }
    }
}
