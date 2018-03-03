using System.Net;
using System.Threading.Tasks;
using TitleSearch.Core.Utility.Interface;

namespace TitleSearch.Core.Utility
{
    public class WebHelper : IWebHelper
    {
        public async Task<string> GetHtmlAsync(string url)
        {
            string htmlCode;
            using (var webclient = new WebClient())
            {
                htmlCode = await webclient.DownloadStringTaskAsync(url);
            }
            return htmlCode;
        }
    }
}
