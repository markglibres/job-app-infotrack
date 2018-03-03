using System.Threading.Tasks;

namespace TitleSearch.Core.Utility.Interface
{
    public interface IWebHelper
    {
        Task<string> GetHtmlAsync(string url);
    }
}
