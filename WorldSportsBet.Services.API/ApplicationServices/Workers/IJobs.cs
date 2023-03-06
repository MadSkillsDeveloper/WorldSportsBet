using Hangfire.Server;

namespace WorldSportsBet.Services.API.ApplicationServices.Workers
{
    public interface IJobs
    {
        Task PollCurrencyApi(PerformContext performContext);
        Task SendData(PerformContext performContext);
    }
}
