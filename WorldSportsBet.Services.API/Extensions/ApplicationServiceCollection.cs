using WorldSportsBet.Services.API.ApplicationServices;
using WorldSportsBet.Services.API.ApplicationServices.ExternalServices;
using WorldSportsBet.Services.API.Models;
using WorldSportsBetting.Services.API.ApplicationServices;
using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBetting.Services.API.Extensions
{
    public static class ApplicationServiceCollection
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static IServiceCollection ConfigureApplicationServices(
            this WebApplicationBuilder builder,
            string mongoDbSection,
            string apiSettingsSection)
        {
            var apiSection = builder.Configuration.GetSection(apiSettingsSection);
            builder.Services.Configure<APISettings>(apiSection);
            builder.Services.AddScoped<IDataService<CurrencyRateModel>, DataService<CurrencyRateModel>>();

            var mongoSection = builder.Configuration.GetSection(mongoDbSection);
            builder.Services.Configure<MongoDbSettings>(mongoSection);
            builder.Services.AddScoped<ICurrencyRateService, CurrencyRateService>();

            return builder.Services;
        }
        #endregion

        #region Constructors
        #endregion
    }
}
