using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Request
{
    public class LangSearchRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}
