using Dotnet.LangSearch.SDK.Models.Request;
using Dotnet.LangSearch.SDK.Models.Response;

namespace Dotnet.LangSearch.SDK.Interfaces
{
    public interface ILangSearchClient
    {
        Task<LangSearchWebResponse> GetWebSearchResponse(WebSearchRequest request);
        Task<LangSearchRankedResponse> GetRankedSearchResponse(RankedSearchRequest request);
    }
}
