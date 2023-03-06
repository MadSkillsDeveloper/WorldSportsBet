using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using WorldSportsBet.Services.API.ApplicationServices;
using WorldSportsBet.Services.API.ApplicationServices.ExternalServices;
using WorldSportsBet.Services.API.Models;
using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBetting.Services.Test
{
    public class ApplicationServicesTest
    {
        #region Fields
        private IDataService<CurrencyRateModel> _dataService;
        private readonly Mock<IOptions<APISettings>> _apiSettingsMock = new();
        readonly Dictionary<string, string> _headers = new()
            {
                { "apiKey", "5syfODv4u91ezx8kUB3FxMFWYSGEmOeC" }
            };
        #endregion

        #region Properties
        #endregion

        #region Methods
        [Fact]
        public void GivenAValidURLWithParameters_ShouldReturnData()
        {
            //Arrange
            _apiSettingsMock.Setup(m => m.Value).Returns(
                new APISettings
                {
                    URL = "https://api.apilayer.com/fixer",
                    Headers = _headers
                });

            _dataService = new DataService<CurrencyRateModel>(_apiSettingsMock.Object);

            Dictionary<string, string> urlSegments = new()
            {
                {"symbols", "EUR, GBP, JPY" },
                {"base", "ZAR" }
            };

            //Act
            var sut = _dataService.Get("/latest", urlSegments);

            //Assert
            sut.Base.Should().Be("ZAR");
        }
        #endregion

        #region Constructors
        #endregion
    }
}