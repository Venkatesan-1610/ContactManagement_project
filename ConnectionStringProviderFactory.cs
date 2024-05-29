using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmployeeDetails.Services
{
    public class ConnectionStringProviderFactory : IDesignTimeDbContextFactory<ConnectionStringProvider>
    {
        public ConnectionStringProvider CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConnectionStringProvider>();

            // Read the configuration from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ConnectionStringProvider(optionsBuilder.Options);
        }
    }
}
