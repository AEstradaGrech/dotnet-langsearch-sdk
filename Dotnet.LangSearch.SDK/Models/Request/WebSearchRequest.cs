using Dotnet.LangSearch.SDK.Models.Enums;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Request
{
    public class WebSearchRequest
    {
        public WebSearchRequest() { }
        public WebSearchRequest(string query, int results, bool? isFullText = true, EQueryFreshness freshness = EQueryFreshness.NoLimit)
        {
            Query = query;
            Count = results;
            Freshness = freshness;

            if(isFullText.HasValue)
                Contents = new Dictionary<string, object> { { "text", isFullText.Value } };
        }

        public WebSearchRequest(string query, int results, int maxTextCharacters, EQueryFreshness freshness = EQueryFreshness.NoLimit) : this(query, results, false, freshness)
        {
            Contents = new Dictionary<string, object> { { "text", new { maxCharacters = maxTextCharacters } } };
        }
        [JsonIgnore]
        public bool? IsFullText => Contents == null ? null : 
            Contents.ContainsKey("text") ?
            Contents["text"] is bool ?
            (bool?)Contents["text"] : true : null;

        [JsonPropertyName("query")]
        public string Query { get; set; }

        /// <summary>
        /// true enables full webpage text, capped at 5000 characters per result by default. 
        /// An object enables text and optionally sets maxCharacters; no separate true flag is needed. 
        /// false or omission uses snippet mode. In text mode, text replaces snippet.
        /// </summary>
        public Dictionary<string, object>? Contents { get; set; }

        /// <summary>
        /// Specifies the time range for search results. Possible values:
        ///     - oneDay: Results from the past 24 hours.
        ///     - oneWeek: Results from the past week.
        ///     - oneMonth: Results from the past month.
        ///     - oneYear: Results from the past year.
        ///     - noLimit: No time filter(default).
        /// </summary>
        
        [JsonPropertyName("freshness")]
        public EQueryFreshness Freshness { get; set; }
        /// <summary>
        /// The number of results to return. Possible range: 1-10 (default is 10).
        /// </summary>
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        /// <summary>
        /// Restrict results to these domains. Omit or use an empty array for no inclusion filter.
        /// Minimum string length: 1
        /// </summary>
        public List<string>? IncludeDomains { get; set; }

        /// <summary>
        /// Exlude results from this domains. Omit or use an empty array for no exclusion filter.
        /// Minimum string length: 1
        /// </summary>
        public List<string>? ExcludeDomains { get; set; }
    }
}
