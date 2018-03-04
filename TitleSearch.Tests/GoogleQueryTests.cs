using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TitleSearch.Core.Utility;
using TitleSearch.Core.Utility.Interface;

namespace TitleSearch.Tests
{
    [TestClass]
    public class GoogleQueryTests
    {
        private readonly IGoogleQuery _googleQuery;
        private readonly IWebHelper _webHelper;

        public GoogleQueryTests()
        {
            _webHelper = new WebHelper();
            _googleQuery = new GoogleQuery(_webHelper);
        }

        [TestMethod]
        public async Task GetGoogleResults_ShouldReturn10ResultItems()
        {
            var results = await _googleQuery.GetHtmlResults("test", 1);
            Assert.AreEqual(10, results.Count());
        }

        [TestMethod]
        public async Task GetGoogleUrlResult_ShouldReturnValidUrl()
        {
            var results = await _googleQuery.GetHtmlResults("test", 1);
            var url = _googleQuery.GetUrlResult(results.First());
            Assert.AreEqual(true, Uri.IsWellFormedUriString(url, UriKind.Absolute));
        }
    }
}