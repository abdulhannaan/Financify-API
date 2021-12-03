using Microsoft.Extensions.Configuration;
using System;

namespace Data.Utils
{
    public static class Connection
    {
        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();
            return configuration.GetConnectionString("AHDDatabase");
        }
    }
}
