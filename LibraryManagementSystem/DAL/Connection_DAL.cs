using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.DAL
{
    public class Connection_DAL
    {
        public static IConfiguration? Configuration { get; set; }

        public static string ConnectionString()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }
    }
}
