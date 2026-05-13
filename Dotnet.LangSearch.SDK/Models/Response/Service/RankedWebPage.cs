using Dotnet.LangSearch.SDK.Models.Response.WebSearch;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.Service
{
    public class RankedWebPage
    {
        public RankedWebPage() { }
        public RankedWebPage(WebPageValue page, int rankIdx, float score)
        {
            Id = page.Id;
            Name = page.Name;
            Url = page.Url;
            Snippet = page.Snippet;
            Summary = page.Summary;
            Index = rankIdx;
            Score = score;
        }
        /// <summary>
        /// Unique identifier for the web page.
        /// Return value example: https://api.langsearch.com/v1/web-search#1
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The title of the webpage.
        /// Return value example: ESG Report June 2024 - Apple Inc. (AAPL)
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The URL of the webpage.
        /// Return value example: https://www.crispidea.com/report/esg-report-june-2024-apple/
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// A brief snippet from the web page. 
        /// Returns a long text.
        /// </summary>
        [JsonPropertyName("snippet")]
        public string Snippet { get; set; }
        /// <summary>
        /// Full summary (request option)
        /// </summary>
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("relevance_score")]
        public float Score { get; set; }
    }
}
