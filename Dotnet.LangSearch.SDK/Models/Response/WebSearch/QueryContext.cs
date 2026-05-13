using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.WebSearch
{
    public class QueryContext
    {
        [JsonPropertyName("originalQuery")]
        public string OriginalQuery { get; set; }
    }
}
