using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response.WebSearch;

namespace Dotnet.LangSearch.SDK
{
    public interface ILangSearchService
    {
        Task<SearchData> GetWebSearchData(WebSearchRequest request);
        Task<WebSearchResult> GetWebPage(WebSearchRequest request);
        Task<List<WebPageValue>> GetWebSearchResults(WebSearchRequest request);
        Task<List<string>> SearchWebTexts(WebSearchRequest request);
    }
}
