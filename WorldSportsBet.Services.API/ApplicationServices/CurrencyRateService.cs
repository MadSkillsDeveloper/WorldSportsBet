using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBetting.Services.API.ApplicationServices
{
    public class CurrencyRateService : ICurrencyRateService
    {
        #region Fields
        private readonly IMongoCollection<CurrencyRate> _currencies;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public async Task<List<CurrencyRate>> GetCurrencyRatesAsync()
        {
            return await _currencies.Find(_ => true).ToListAsync();
        }

        public async Task CreateCurrencyRate(CurrencyRate currencyRate)
        {
            await _currencies.InsertOneAsync(currencyRate);
        }
        #endregion

        #region Constructors
        public CurrencyRateService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(
            mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbSettings.Value.DatabaseName);

            _currencies = mongoDatabase.GetCollection<CurrencyRate>(
                mongoDbSettings.Value.CollectionName);
        }
        #endregion

    }
}
