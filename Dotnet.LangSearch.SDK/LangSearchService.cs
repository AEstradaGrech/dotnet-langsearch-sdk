using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.RankedSearch;
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

        public async Task<WebPage> GetWebPage(WebSearchRequest request)
        {
            var data = await GetWebSearchData(request);

            return data.WebPages;
        }

        public async Task<SearchData> GetWebSearchData(WebSearchRequest request)
        {
            var response = await _client.GetWebSearchResponse(request);

            return response.Data;
        }

        public async Task<RankedData> GetReRankData(RankedSearchRequest request)
        {
            var data = await _client.GetRankedSearchResponse(request);

            return new RankedData { Model = data.Model, Query = request.Query, Results = data.Results.Select(doc => new RankedDocument { Index = doc.Index, Score = doc.Score, Text = doc.Document.Text }).ToList() };
        }
    }
}
