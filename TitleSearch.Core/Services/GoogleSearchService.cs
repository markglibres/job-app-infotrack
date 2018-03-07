using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TitleSearch.Core.Models;
using TitleSearch.Core.Services.Interface;
using TitleSearch.Core.Utility.Interface;

namespace TitleSearch.Core.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IDomainService _domainService;
        private readonly IGoogleQuery _googleQuery;
        
        public GoogleSearchService(IDomainService domainService,
            IGoogleQuery googleQuery)
        {
            _domainService = domainService;
            _googleQuery = googleQuery;
        }

        public async Task<IEnumerable<GoogleRankResult>> GetRankingsAsync(string url, string searchTerm,
            int maxRank = 100)
        {
            var rankings = new List<GoogleRankResult>();
            var maxPage = _googleQuery.TotalPage(maxRank);

            for (var page = 1; page <= maxPage; page++)
            {
                var pageRankings = await _googleQuery.GetRankingsAsync(searchTerm, page);
                rankings.AddRange(pageRankings.Where(ranking => _domainService.IsSameHost(url,ranking.Url)));
                
            }

            return rankings;
        }
        
    }
}
