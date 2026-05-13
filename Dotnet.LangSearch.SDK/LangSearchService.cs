using Dotnet.LangSearch.SDK.Interfaces;
using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.RankedSearch;
using Dotnet.LangSearch.SDK.Models.Response.Service;
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

            return data.WebPage;
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

        public async Task<RankedData> GetReRankData(RankedSearchRequest request)
        {
            var data = await _client.GetRankedSearchResponse(request);

            return new RankedData { Model = data.Model, Query = request.Query, Results = data.Results.Select(doc => new RankedDocument { Index = doc.Index, Score = doc.Score, Text = doc.Document.Text }).ToList() };
        }

        public async Task<List<RankedWebPage>> SearchAndRankPages(WebSearchRequest request)
        {
            request.Summary = true;

            if (request.Count == null)
                request.Count = 10;

            request.Count = Math.Min(request.Count.Value, 10);
            //ResultsNumber defaults to 10, and 10 is the maximum
            var response = await GetWebPage(request);

            //ResultsNumber defaults to total number of documents passed in the request
            var rankRequest = new RankedSearchRequest
            {
                Query = request.Query,
                QueriedDocuments = response.Results.Select(value => value.Summary).ToList(),
                WithDocuments = true
            };

            var rankedPages = await GetReRankData(rankRequest);

            return response.Results.Select(page =>
                new RankedWebPage(
                    page,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Index,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Score))
                .OrderByDescending(page => page.Score)
                .ToList();
        }

        public async Task<List<RankedWebPage>> SearchAndRankPages(RankedPageRequest request)
        {
            if (request.Count == null)
                request.Count = 10;

            var webRequest = new WebSearchRequest
            {
                Query = request.Query,
                Count = Math.Min(request.Count.Value, 10),
                Freshness = request.Freshness,
                Summary = true
            };

            var page = await GetWebPage(webRequest);

            var rankRequest = new RankedSearchRequest
            {
                Query = request.Query,
                Model = request.Model,
                WithDocuments = true,
                QueriedDocuments = page.Results.Select(page => page.Summary).ToList(),
            };

            var rankedPages = await GetReRankData(rankRequest);

            return request.ScoreThreshold == null ?
                page.Results.Select(page => new RankedWebPage(
                    page,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Index,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Score))
                .OrderByDescending(page => page.Score)
                .ToList() :
                page.Results.Select(page => new RankedWebPage(
                    page,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Index,
                    rankedPages.Results.SingleOrDefault(result => result.Text == page.Summary).Score))
                .Where(rankedPage => rankedPage.Score >= request.ScoreThreshold)
                .OrderByDescending(page => page.Score)
                .ToList();
        }
    }
}
