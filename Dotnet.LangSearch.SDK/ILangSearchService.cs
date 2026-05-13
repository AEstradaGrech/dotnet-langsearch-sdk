using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.RankedSearch;
using Dotnet.LangSearch.SDK.Models.Response.WebSearch;

namespace Dotnet.LangSearch.SDK
{
    public interface ILangSearchService
    {
        Task<SearchData> GetWebSearchData(WebSearchRequest request);
        Task<WebPage> GetWebPage(WebSearchRequest request);

        Task<RankedData> GetReRankData(RankedSearchRequest request);

        //TODO: SearchAndRank with LangSearch
    }
}
