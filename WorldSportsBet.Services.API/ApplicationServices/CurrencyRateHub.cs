using Microsoft.AspNetCore.SignalR;

namespace WorldSportsBet.Services.API.ApplicationServices
{
    public class CurrencyRateHub : Hub
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Methods
        public async Task SendMessage(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            await Clients.All.SendAsync(message);
        }
        #endregion

        #region Constructors
        #endregion
    }
}
