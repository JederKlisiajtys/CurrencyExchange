//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace CurrencyExchange.DataAccess
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CurrencyExchangeContext>
//    {

//        public CurrencyExchangeContext CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                 .SetBasePath(Directory.GetCurrentDirectory())
//                 .AddJsonFile("appsettings.json")
//                 .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<CurrencyExchangeContext>();
//            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

//            return new CurrencyExchangeContext(optionsBuilder.Options);
//        }
//    }
//}
