using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Ordering.Infrastructure.Data
{
    public class OrderContextfactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            //Load configuration
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            // use configuration here
            var connectionString =
                configuration.GetConnectionString("OrderingConnectionString");

            // Configure DbContext with retry logic
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5, // Number of retries before failing
                    maxRetryDelay: TimeSpan.FromSeconds(10), // Max delay between retries
                    errorNumbersToAdd: null
                );
            });

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
