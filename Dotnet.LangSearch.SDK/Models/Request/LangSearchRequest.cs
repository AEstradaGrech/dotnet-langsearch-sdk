using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Request
{
    public class LangSearchRequest
    {
        public LangSearchRequest() { }
        public LangSearchRequest(string query) { Query = query; }
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
