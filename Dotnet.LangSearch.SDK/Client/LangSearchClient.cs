using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models;
using Dotnet.LangSearch.SDK.Models.Enums;
using Dotnet.LangSearch.SDK.Models.Exceptions;
using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Dotnet.LangSearch.SDK.Client
{
    public class LangSearchClient : ILangSearchClient
    {
        private readonly LangSearchSettings _settings;
        private readonly HttpClient _httpClient;
        public LangSearchClient(HttpClient client, IOptions<LangSearchSettings> settings) : base() 
        { 
            _httpClient = client;
            _settings = settings.Value ?? throw new ArgumentNullException(nameof(LangSearchSettings));
        }

        public async Task<LangSearchWebResponse> GetWebSearchResponse(WebSearchRequest request)
        {
            try
            {
                request.Query = request.Query.Trim();

                if (string.IsNullOrEmpty(request.Query))
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> Invalid Query >> Query is empty");

                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
                
                options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                
                var response = await _httpClient.PostAsJsonAsync<WebSearchRequest>($"/{_settings.WebSearchEndpoint}", request, options);

                if (!response.IsSuccessStatusCode)
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {nameof(GetWebSearchResponse)} >> an error has occured while requesting the data");

                var data = await response.Content.ReadFromJsonAsync<LangSearchWebResponse>();

                if (data.Code != HttpStatusCode.OK)
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {data.ErrorMessage}");

                return data;
            }
            catch(Exception ex)
            {
                if (ex.GetType() != typeof(LangSearchClientException))
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> An error has occured while making the request to endpoint: {_settings.WebSearchEndpoint} >> {ex.Message}");

                throw ex;
            }
        }

        public async Task<LangSearchRankedResponse> GetRankedSearchResponse(RankedSearchRequest request)
        {
            try
            {
                request.Query = request.Query.Trim();
                
                if (string.IsNullOrEmpty(request.Query))
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> Invalid Query >> Query is empty");

                request.QueriedDocuments = request.QueriedDocuments.Where(doc => !string.IsNullOrEmpty(doc)).ToList();

                if (request.QueriedDocuments.Count == 0)
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> No documents to re-rank present in the request");

                if (request.ResultsNumber == null)
                    request.ResultsNumber = request.QueriedDocuments.Count;

                request.ResultsNumber = request.ResultsNumber = Math.Clamp(request.QueriedDocuments.Count, 0, 10);

                if (string.IsNullOrEmpty(request.Model))
                    request.Model = _settings.DefaultRerankModel;

                var response = await _httpClient.PostAsJsonAsync<RankedSearchRequest>($"/{_settings.RankedSearchEndpoint}", request, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                if (!response.IsSuccessStatusCode)
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {nameof(GetRankedSearchResponse)} >> an error has occured while requesting the data >> STATUS CODE: {response.StatusCode}");

                var data = await response.Content.ReadFromJsonAsync<LangSearchRankedResponse>();

                if (data.Code != HttpStatusCode.OK)
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {data.ErrorMessage}");

                return data;
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(LangSearchClientException))
                    throw new LangSearchClientException($"{nameof(LangSearchClient)} >> An error has occured while making the request to endpoint: {_settings.RankedSearchEndpoint} >> {ex.Message}");

                throw ex;
            }
        }
    }
}
