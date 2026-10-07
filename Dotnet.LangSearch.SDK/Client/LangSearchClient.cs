using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models;
using Dotnet.LangSearch.SDK.Models.Exceptions;
using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
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

                var jsonResponse = await response.Content.ReadFromJsonAsync<JsonObject>();

                // 07/10/2026 --> why this weird parsing process? because the API returns this as error response:
                //  {"success":false,"code":"500","subCode":null,"msg":"Runtime Exception","data":null,"timestamp":1791380247620,"enableThrow":true,"enableRespException":false }
                // but status is integer code is 200
                if (jsonResponse.TryGetPropertyValue("code", out var codeProperty) && codeProperty is JsonValue codeValue)
                {
                    if (codeValue.TryGetValue<int>(out int codeInt))
                        if (codeInt != (int)HttpStatusCode.OK)
                            throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {getResponseErrorMessage(jsonResponse)}");

                    if (codeValue.TryGetValue<string>(out string codeString))
                        if (!Enum.TryParse<HttpStatusCode>(codeString, true, out var codeEnum) || codeEnum != HttpStatusCode.OK)
                            throw new LangSearchClientException($"{nameof(LangSearchClient)} >> {getResponseErrorMessage(jsonResponse)}");
                }

                else throw new LangSearchClientException($"{nameof(LangSearchClient)} >> Invalid response format: 'code' property is missing or invalid");
                
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

        private string getResponseErrorMessage(JsonObject jsonResponse)
            => jsonResponse.TryGetPropertyValue("msg", out var errorMessageProperty) &&
                errorMessageProperty is JsonValue errorMessageValue &&
                errorMessageValue.TryGetValue(out string? errorMessageString) ?
                errorMessageString : "Unknown error";
        
    }
}
