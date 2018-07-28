using System.Net;

namespace ImageMege
{
    public class DataDownloader
    {
        public string DownloadDataFromUrl(string url, WebClient webClient)
        {
            return webClient.DownloadString(url);
        }
    }
}