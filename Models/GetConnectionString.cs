using Microsoft.Extensions.Configuration;
using System.IO;

namespace NBAwebsite.Models
{
    public static class GetConnectionString
    {
        public static string conString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            string constring = config.GetConnectionString("CSforNBAwebDB");
            return constring;

            //return "server=MISHOPC\\SQLEXPRESS; database=NBAWebsiteDB; integrated security = true";
        }
    }
}