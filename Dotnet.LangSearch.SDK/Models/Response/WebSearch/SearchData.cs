using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.WebSearch
{
    public class SearchData
    {
        [JsonPropertyName("queryContext")]
        public QueryContext QueryContext { get; set; }
        [JsonPropertyName("webPages")]
        public WebPage WebPages { get; set; }
    }
}
