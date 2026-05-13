using Dotnet.LangSearch.SDK.Models.Response.RankedSearch;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response
{
    public class LangSearchRankedResponse : LangSearchResponse
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("results")]
        public List<RankedResult> Results { get; set; }
    }
}
