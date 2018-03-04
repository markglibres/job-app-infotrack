using System.Collections.Generic;
using System.Threading.Tasks;
using TitleSearch.Core.Models;

namespace TitleSearch.Core.Utility.Interface
{
    public interface IGoogleQuery
    {
        int ItemsPerPage { get; }
        string BuildUrl(string searchTerm, int page);
        int TotalPage(int maxRank);
        Task<IEnumerable<string>> GetHtmlResults(string searchTerm, int page);
        string GetUrlResult(string htmlResult);
        Task<IEnumerable<GoogleRankResult>> GetRankingsAsync(string searchTerm, int page);
    }
}
