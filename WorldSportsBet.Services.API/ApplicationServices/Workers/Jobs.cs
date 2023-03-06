using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.SignalR;
using WorldSportsBet.Services.API.Models;
using WorldSportsBetting.Services.API.ApplicationServices;
using WorldSportsBetting.Services.API.Mappings;

namespace WorldSportsBet.Services.API.ApplicationServices.Workers
{
    public class Jobs : IJobs
    {
        #region Fields
        private readonly IHubContext<CurrencyRateHub> _hubContext;
        private readonly ICurrencyRateService _currencyRateService;
        private readonly IDataService<CurrencyRateModel> _dataService;
        #endregion

        #region Properties
        #endregion

        #region Methods
        [Queue("default")]
        [JobDisplayName("Poll Currency API")]
        public async Task PollCurrencyApi(PerformContext performContext)
        {
            Dictionary<string, string> urlSegments = new()
            {
                {"symbols", "" },
                {"base", "EUR" }
            };

            var currencyRateServiceData = _dataService.Get("/latest", urlSegments);
            var currencyRate = currencyRateServiceData.Map();
            await _currencyRateService.CreateCurrencyRate(currencyRate);
        }

        [Queue("queue2")]
        [JobDisplayName("Send Data To App")]
        public async Task SendData(PerformContext performContext)
        {
            var data = await _currencyRateService.GetCurrencyRatesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveData", data);
        }
        #endregion

        #region Constructors
        public Jobs(
            IHubContext<CurrencyRateHub> hubContext,
            ICurrencyRateService currencyRateService,
            IDataService<CurrencyRateModel> dataService)
        {
            _hubContext = hubContext;
            _currencyRateService = currencyRateService;
            _dataService = dataService;
        }
        #endregion

    }
}
