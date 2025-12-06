using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using wms_android.shared.Data;

namespace wms_android.api.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Build configuration using the same hierarchy as the main application
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            Console.WriteLine($"Design-time: Using connection string from configuration");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsAssembly("wms_android.api");
                options.EnableRetryOnFailure(3);
            });

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}