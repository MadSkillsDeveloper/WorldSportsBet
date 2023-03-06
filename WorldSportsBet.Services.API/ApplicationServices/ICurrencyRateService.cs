using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBetting.Services.API.ApplicationServices
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRate>> GetCurrencyRatesAsync();
        Task CreateCurrencyRate(CurrencyRate currencyRate);
    }
}
