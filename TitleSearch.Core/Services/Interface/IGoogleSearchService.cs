using System.Collections.Generic;
using System.Threading.Tasks;
using TitleSearch.Core.Models;

namespace TitleSearch.Core.Services.Interface
{
    public interface IGoogleSearchService
    {
        Task<IEnumerable<GoogleRankResult>> GetRankingsAsync(string url, string searchTerm, int maxRank = 100);
    }
}
