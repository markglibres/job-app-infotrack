using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using TitleSearch.Core.Models;
using TitleSearch.Core.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TitleSearch.Api
{
    [Route("api/search")]
    public class SearchApiController : Controller
    {
        private readonly IGoogleSearchService _searchService;
        public SearchApiController(IGoogleSearchService searchService)
        {
            _searchService = searchService;
        }

        [Route("rankings")]
        [HttpGet]
        public async Task<IEnumerable<GoogleRankResult>> Get([FromQuery] string url, string searchTerm)
        {
            return await _searchService.GetRankingsAsync(HttpUtility.UrlDecode(url), HttpUtility.UrlDecode(searchTerm));
        }
        
    }
}
