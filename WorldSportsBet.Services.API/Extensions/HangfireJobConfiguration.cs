using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using WorldSportsBet.Services.API.ApplicationServices.Workers;

namespace WorldSportsBet.Services.API.Extensions
{
    public static class HangfireJobConfiguration
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static void ConfigureJobs(this WebApplication app, string connectionString)
        {
            try
            {
                var storage = JobStorage.Current;
            }
            catch
            {
                GlobalConfiguration.Configuration
                    .UseMongoStorage(connectionString, new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                            BackupStrategy = new CollectionMongoBackupStrategy()
                        },
                        Prefix = "Jobs",
                        CheckConnection = false
                    });
            }
            finally
            {
                using IServiceScope scope = app.Services.CreateScope();
                IServiceProvider services = scope.ServiceProvider;
                RecurringJob.AddOrUpdate(() => services.GetService<IJobs>().PollCurrencyApi(null), "*/5 * * * *", TimeZoneInfo.Local);
            }
        }

        #endregion

        #region Constructors
        #endregion
    }
}
