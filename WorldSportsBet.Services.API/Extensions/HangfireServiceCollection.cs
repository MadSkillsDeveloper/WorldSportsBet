using Hangfire;
using Hangfire.Console;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;

namespace WorldSportsBet.Services.API.Extensions
{
    public static class HangfireServiceCollection
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static void ConfigureHangfireServices(
            this WebApplicationBuilder builder, string connectionString)
        {

            builder.Services.AddHangfire(x => x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseConsole()
                    .UseMongoStorage(connectionString, new MongoStorageOptions
                    {
                        MigrationOptions = new MongoMigrationOptions
                        {
                            MigrationStrategy = new MigrateMongoMigrationStrategy(),
                            BackupStrategy = new CollectionMongoBackupStrategy()
                        },
                        Prefix = "Jobs",
                        CheckConnection = false
                    }));

            builder.Services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "localhost";
            });
        }
        #endregion

        #region Constructors
        #endregion
    }
}
