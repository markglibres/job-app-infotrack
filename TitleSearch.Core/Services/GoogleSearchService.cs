using System.Collections.Generic;
using System.Threading.Tasks;
using TitleSearch.Core.Models;
using TitleSearch.Core.Services.Interface;
using TitleSearch.Core.Utility.Interface;

namespace TitleSearch.Core.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        public GoogleSearchService(IDomainService domainService,
            IGoogleQuery googleQuery)
        {
            _domainService = domainService;
            _googleQuery = googleQuery;
        }

        private IDomainService _domainService { get; }
        private IGoogleQuery _googleQuery { get; }

        public async Task<IEnumerable<GoogleRankResult>> GetRankingsAsync(string url, string searchTerm,
            int maxRank = 100)
        {
            var rankings = new List<GoogleRankResult>();

            var maxPage = _googleQuery.TotalPage(maxRank);
            var currentRank = 0;

            for (var page = 1; page <= maxPage; page++)
            {
                var results = await _googleQuery.GetHtmlResults(searchTerm, page);
                foreach (var result in results)
                {
                    currentRank++;
                    var resultUrl = _googleQuery.GetUrlResult(result);
                    if (_domainService.IsSameHost(url, resultUrl))
                    {
                        rankings.Add(new GoogleRankResult
                        {
                            Page = page,
                            Rank = currentRank,
                            Url = resultUrl
                        });
                    }
                }
            }

            return rankings;
        }
    }
}