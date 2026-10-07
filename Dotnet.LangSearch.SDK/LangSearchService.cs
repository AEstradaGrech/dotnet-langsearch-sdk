using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.WebSearch;

namespace Dotnet.LangSearch.SDK
{
    public class LangSearchService : ILangSearchService
    {
        private readonly ILangSearchClient _client;

        public LangSearchService(ILangSearchClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<WebSearchResult> GetWebPage(WebSearchRequest request)
        {
            var data = await GetWebSearchData(request);

            return data.Result;
        }

        public async Task<List<WebPageValue>> GetWebSearchResults(WebSearchRequest request)
        {
            var data = await GetWebPage(request);

            return data.Results;
        }
        public async Task<SearchData> GetWebSearchData(WebSearchRequest request)
        {
            var response = await _client.GetWebSearchResponse(request);

            return response.Data;
        }

        public async Task<List<string>> SearchWebTexts(WebSearchRequest request)
        {
            var results = await GetWebSearchResults(request);

            bool isFullText = request.IsFullText.HasValue && request.IsFullText.Value;
            return results.Count == 0 ? [] : 
                results.Where(x => isFullText ? !string.IsNullOrEmpty(x.FullText) : !string.IsNullOrEmpty(x.Snippet))
                       .Select(result => request.IsFullText.HasValue && request.IsFullText.Value ? result.FullText : result.Snippet)
                       .ToList();
        }
    }
}
