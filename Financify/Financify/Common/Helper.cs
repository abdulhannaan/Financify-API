using Microsoft.Extensions.Configuration;
using System.IO;

namespace Financify.Common
{
    public static class Connection
    {
       // ignore connection request, if already connected
        public static IConfiguration Configuration { get; set; }

        public static string GetConnectionString()
        {
            string connectionString = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();  

            string strDbConnType = Configuration.GetSection("AppSettings").GetSection("DbConnectionType").Value;// AppSettings["DbConnectionType"];
            string strDatabase = Configuration.GetSection("AppSettings").GetSection("Database").Value;// ConfigurationManager.AppSettings["Database"];
            string strConnEncrypted = Configuration.GetSection("AppSettings").GetSection("DbConnectionString").Value; // ConfigurationManager.AppSettings["DbConnectionString"];

            if (strDatabase.ToLower() == "sql")
            {
                string server = Configuration.GetSection("AppSettings").GetSection("dbServer").Value;//ConfigurationManager.AppSettings["dbServer"];
                string dbName = Configuration.GetSection("AppSettings").GetSection("dbName").Value;//ConfigurationManager.AppSettings["dbName"];
                string dbUser = Configuration.GetSection("AppSettings").GetSection("dbUser").Value;//ConfigurationManager.AppSettings["dbUser"];
                string dbPassword = Configuration.GetSection("AppSettings").GetSection("dbPassword").Value;//ConfigurationManager.AppSettings["dbPassword"];                
                connectionString = "Data Source=" + server + ";Initial Catalog=" + dbName + ";uid=" + dbUser + ";pwd=" + dbPassword;
            }

            return connectionString;
        }

        public static string GetConnectionString(string dbType, string dbName, string dbUser, string dbPassword)
        {
            string connectionString = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            string strDbConnType = Configuration.GetSection("AppSettings").GetSection("DbConnectionType").Value;// AppSettings["DbConnectionType"];
            string strDatabase = Configuration.GetSection("AppSettings").GetSection("Database").Value;// ConfigurationManager.AppSettings["Database"];
            string strConnEncrypted = Configuration.GetSection("AppSettings").GetSection("DbConnectionString").Value; // ConfigurationManager.AppSettings["DbConnectionString"];

            if (dbType.ToLower() == "sql")
            {
                string server = Configuration.GetSection("AppSettings").GetSection("dbServer").Value;//ConfigurationManager.AppSettings["dbServer"];

                connectionString = "Data Source=" + server + ";Initial Catalog=" + dbName + ";uid=" + dbUser + ";pwd=" + dbPassword;
            }

            return connectionString;
        }
          
    }
}