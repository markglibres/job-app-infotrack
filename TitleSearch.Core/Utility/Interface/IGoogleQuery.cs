using System.Collections.Generic;
using System.Threading.Tasks;

namespace TitleSearch.Core.Utility.Interface
{
    public interface IGoogleQuery
    {
        string BuildUrl(string searchTerm, int page);
        int TotalPage(int maxRank);
        Task<IEnumerable<string>> GetHtmlResults(string searchTerm, int page);
        string GetUrlResult(string htmlResult);
    }
}
