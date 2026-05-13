using Dotnet.LangSearch.SDK.Models.Response.WebSearch;
using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response
{
    public class LangSearchWebResponse : LangSearchResponse
    {
        
        [JsonPropertyName("data")]
        public SearchData Data { get; set; }
    }
}
