namespace TitleSearch.Core.Services.Interface
{
    public interface IDomainService
    {
        bool IsSameHost(string urlSource, string urlToCheck);
        string GetHost(string url);
    }
}
