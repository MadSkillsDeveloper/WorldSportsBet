namespace WorldSportsBet.Services.API.Models
{
    public class CurrencyRateModel
    {
        #region Fields
        #endregion

        #region Properties
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public IDictionary<string, decimal> Rates { get; set; }
        public int Timestamp { get; set; }
        public bool Success { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Constructors
        #endregion
    }
}
