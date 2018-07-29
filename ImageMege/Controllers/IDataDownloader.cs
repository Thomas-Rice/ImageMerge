using System.Net;

namespace ImageMege.Controllers
{
    public interface IDataDownloader
    {
        string DownloadDataFromUrl(string imagesUrl, WebClient webClient);
    }
}
