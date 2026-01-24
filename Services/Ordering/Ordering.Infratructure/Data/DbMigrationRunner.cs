using DbUp;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data
{
    public static class DbMigrationRunner
    {
        public static void Run(string connectionString, ILogger logger)
        {
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(
                    typeof(DbMigrationRunner).Assembly)
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                logger.LogError(result.Error, "Database migration failed");
                throw result.Error;
            }

            logger.LogInformation("Database migration completed successfully");
        }
    }
}
