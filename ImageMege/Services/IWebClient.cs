using System.Net;

namespace ImageMege.Services
{
    public interface IWebClient
    {
        string DownloadString(string url);
    }

    public class WebClientWrapper : IWebClient
    {
        private readonly WebClient _webClient;

        public WebClientWrapper()
        {
            _webClient = new WebClient();
        }

        public string DownloadString(string url)
        {
            return _webClient.DownloadString(url);
        }
    }


}