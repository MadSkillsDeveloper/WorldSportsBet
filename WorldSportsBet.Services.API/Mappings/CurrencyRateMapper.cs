using WorldSportsBet.Services.API.Models;
using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBetting.Services.API.Mappings
{
    public static class CurrencyRateMapper
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public static CurrencyRate Map(this CurrencyRateModel currencyRateModel)
        {
            return new CurrencyRate
            {
                Base = currencyRateModel.Base,
                CreatedDate = DateTime.UnixEpoch.AddSeconds(currencyRateModel.Timestamp),
                Rates = currencyRateModel.Rates,
            };
        }
        #endregion

        #region Constructors
        #endregion
    }
}
