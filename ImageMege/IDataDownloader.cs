namespace ImageMege
{
    public interface IDataDownloader
    {
        string DownloadDataFromUrl(string url, IWebClient webClient);
    }
}