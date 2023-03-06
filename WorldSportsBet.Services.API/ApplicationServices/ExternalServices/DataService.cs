using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using WorldSportsBetting.Services.API.Models;

namespace WorldSportsBet.Services.API.ApplicationServices.ExternalServices
{
    public class DataService<TEntity> : IDataService<TEntity> where TEntity : class
    {
        #region Fields
        private readonly RestClient _restClient;
        #endregion

        #region Properties
        #endregion

        #region Methods
        public TEntity Get(string route, IDictionary<string, string> urlSegment)
        {
            var request = new RestRequest(route, Method.Get);

            if (urlSegment.Count > 0)
            {
                foreach (var kvp in urlSegment)
                {
                    request.AddQueryParameter(kvp.Key, kvp.Value, true);
                }
            }

            var response = _restClient.Execute(request);

            return JsonConvert.DeserializeObject<TEntity>(response.Content);
        }
        #endregion

        #region Constructors
        public DataService(IOptions<APISettings> apiSettings)
        {
            _restClient = new RestClient(apiSettings.Value.URL);
            _restClient.AddDefaultHeaders(apiSettings.Value.Headers);
        }
        #endregion

    }
}
