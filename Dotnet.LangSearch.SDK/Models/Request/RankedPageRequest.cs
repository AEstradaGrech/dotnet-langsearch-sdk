using Dotnet.LangSearch.SDK.Models.Enums;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Request
{
    public class RankedPageRequest : LangSearchRequest
    {
        public RankedPageRequest() : base() { }
        public RankedPageRequest(string query, int results, EQueryFreshness freshness = EQueryFreshness.NoLimit) : base(query)
        {
            Count = results;
            Freshness = freshness;
        }

        [JsonPropertyName("freshness")]
        public EQueryFreshness Freshness { get; set; }
        /// <summary>
        /// The number of results to return. Possible range: 1-10 (default is 10).
        /// </summary>
        [JsonPropertyName("count")]
        public int? Count { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Min score filter value for the returned ranked values. Null to bypass
        /// </summary>
        public float? ScoreThreshold { get; set; }
    }
}
