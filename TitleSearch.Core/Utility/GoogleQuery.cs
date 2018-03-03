using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using TitleSearch.Core.Utility.Interface;

namespace TitleSearch.Core.Utility
{
    public class GoogleQuery : IGoogleQuery
    {
        private const int _itemsPerPage = 10;
        private const string _resultItemPattern = "<h3\\s+?class=\"r\">(.+?)</h3>";
        private const string _resultItemPrimaryUrlPattern = "href=\"(http[^\"]+)";
        private const string _resultItemSecondaryUrlPattern = "q=([^\"\\s]+)";

        public GoogleQuery(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        private IWebHelper _webHelper { get; }

        private List<string> _googleInjectedParamsPattern => new List<string>
        {
            "&sa=[^&]+",
            "&ved=[^&]+",
            "&usg=[^&]+"
        };

        public string BuildUrl(string searchTerm, int page)
        {
            return
                $"https://www.google.com.au/search?q={HttpUtility.UrlEncode(searchTerm)}&start={(page - 1) * _itemsPerPage}";
        }

        public async Task<IEnumerable<string>> GetHtmlResults(string searchTerm, int page)
        {
            var searchUrl = BuildUrl(searchTerm, page);
            var resultPage = await _webHelper.GetHtmlAsync(searchUrl);

            var results = Regex.Matches(resultPage, _resultItemPattern, RegexOptions.IgnoreCase);

            return results.Select(result => HttpUtility.HtmlDecode(result.ToString()));
        }

        public string GetUrlResult(string htmlResult)
        {
            var match = Regex.Match(htmlResult, Regex.IsMatch(htmlResult, _resultItemPrimaryUrlPattern, RegexOptions.IgnoreCase) 
                ? _resultItemPrimaryUrlPattern 
                : _resultItemSecondaryUrlPattern, RegexOptions.IgnoreCase);

            var decodedUrl = HttpUtility.UrlDecode(match.Groups[1].Value);

            _googleInjectedParamsPattern.ForEach(param =>
            {
                decodedUrl = Regex.Replace(decodedUrl, param, string.Empty, RegexOptions.IgnoreCase);
            });

            return decodedUrl;
        }

        public int TotalPage(int maxRank)
        {
            var mod = maxRank % _itemsPerPage;
            return maxRank / _itemsPerPage + (mod > 0 ? 1 : 0);
        }
    }
}