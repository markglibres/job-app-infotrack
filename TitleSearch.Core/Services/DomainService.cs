using System.Text.RegularExpressions;
using TitleSearch.Core.Services.Interface;

namespace TitleSearch.Core.Services
{
    public class DomainService : IDomainService
    {
        private const string _hostPattern = "(?:https?://)?([^/\\?]+)";

        public string GetHost(string url)
        {
            return Regex.Match(url, _hostPattern, RegexOptions.IgnoreCase).Groups[1].Value;
        }

        public bool IsSameHost(string urlSource, string urlToCheck)
        {
            if (string.IsNullOrWhiteSpace(urlSource) || string.IsNullOrWhiteSpace(urlToCheck)) return false;
            return GetHost(urlToCheck).ToLower().Contains(GetHost(urlSource).ToLower());
        }
    }
}