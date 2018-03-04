using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TitleSearch.Api;
using TitleSearch.Core.Services.Interface;
using TitleSearch.Core.Utility;

namespace TitleSearch.Tests
{
    [TestClass]
    public class ApiSearchTests
    {
        [TestMethod]
        public async Task GetRankings_ShouldReturn10Items()
        {
            var googleSearchService = new Mock<IGoogleSearchService>();
            var apiController = new SearchApiController(googleSearchService.Object);

            var webHelper = new WebHelper();
            var googleQuery = new GoogleQuery(webHelper);

            googleSearchService.Setup(service => service.GetRankingsAsync("", "", 100))
                .ReturnsAsync(await googleQuery.GetRankingsAsync("test", 1));

            var results = await apiController.Get("", "");
            Assert.AreEqual(10, results.Count());
        }
    }
}