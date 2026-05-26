using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.RankedSearch;
using Dotnet.LangSearch.SDK.Models.Response.Service;
using Dotnet.LangSearch.SDK.Models.Response.WebSearch;

namespace Dotnet.LangSearch.SDK
{
    public interface ILangSearchService
    {
        Task<SearchData> GetWebSearchData(WebSearchRequest request);
        Task<WebPage> GetWebPage(WebSearchRequest request);
        Task<List<WebPageValue>> GetWebSearchResults(WebSearchRequest request);

        Task<RankedData> GetReRankData(RankedSearchRequest request);

        Task<List<RankedWebPage>> SearchAndRankPages(WebSearchRequest request);

        Task<List<RankedWebPage>> SearchAndRankPages(RankedPageRequest request);

        Task<List<string>> SearchRankedTexts(RankedPageRequest request, bool returnSnippet = false);
    }
}
