using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.WebSearch
{
    public class WebPageValue
    {
        /// <summary>
        /// Unique identifier for the web page.
        /// Return value example: https://api.langsearch.com/v1/web-search#1
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// The title of the webpage.
        /// Return value example: ESG Report June 2024 - Apple Inc. (AAPL)
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// The URL of the webpage.
        /// Return value example: https://www.crispidea.com/report/esg-report-june-2024-apple/
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// The title of the webpage.
        /// Return value example: https://www.crispidea.com/report/esg-report-june-2024-apple/
        /// </summary>
        [JsonPropertyName("displayUrl")]
        public string? DisplayUrl { get; set; }

        /// <summary>
        /// Search snippet when full webpage text is not enabled. Omitted in text mode; available length varies by source.
        /// Returns a long text.
        /// </summary>
        [JsonPropertyName("snippet")]
        public string? Snippet { get; set; }
        /// <summary>
        /// Full webpage text when contents.text is true or an object. Replaces snippet and is capped per result at maxCharacters (default 5000). May be absent when unavailable
        /// </summary>
        [JsonPropertyName("text")]
        public string? FullText { get; set; }

        /// <summary>
        /// The date the page was published.
        /// </summary>
        [JsonPropertyName("datePublished")]
        public DateTime? PublishedDate { get; set; }

    }
}
