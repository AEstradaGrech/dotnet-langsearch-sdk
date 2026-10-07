using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.WebSearch
{
    public class WebSearchResult
    {
        [JsonPropertyName("webSearchUrl")]
        public string Url { get; set; }

        [JsonPropertyName("value")]
        public List<WebPageValue> Results { get; set; }

        /// <summary>
        /// Whether some results were removed due to restrictions.
        /// </summary>

        [JsonPropertyName("someResultsRemoved")]
        public bool? HasRemovedResults { get; set; }
    }
}
