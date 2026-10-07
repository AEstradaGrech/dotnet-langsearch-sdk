using System.Text.Json.Serialization;

namespace Dotnet.LangSearch.SDK.Models.Response.WebSearch
{
    public class TokensUsage
    {
        /// <summary>
        /// Tokens in the search query.
        /// </summary>
        [JsonPropertyName("input_tokens")]
        public int InputTokens { get; set; }

        /// <summary>
        /// Tokens in returned full-length content, or snippets when full-length content is absent. Metadata is excluded
        /// </summary>

        [JsonPropertyName("output_tokens")]
        public int OutputTokens { get; set; }
    }
}
