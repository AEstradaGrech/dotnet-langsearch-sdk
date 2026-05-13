using System.Net;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response
{
    public class LangSearchResponse
    {

        /// <summary>
        /// Http Status Code
        /// </summary>
        [JsonPropertyName("code")]
        public HttpStatusCode Code { get; set; }  

        /// <summary>
        /// Request ID for tracking.
        /// </summary>
        [JsonPropertyName("logId")]
        public string LogId { get; set; }

        /// <summary>
        /// Status message.
        /// </summary>
        [JsonPropertyName("msg")]
        public string? ErrorMessage { get; set; }
    }
}
