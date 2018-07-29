using System.Collections.Generic;
using System.Linq;
using System.Net;
using ImageMege.Models;

namespace ImageMege
{
    public class PagedAlbumCollectionGenerator : IPagedAlbumCollectionGenerator
    {
        private const string ImagesUrl = "http://jsonplaceholder.typicode.com/photos";
        private const string AlbumsUrl = "http://jsonplaceholder.typicode.com/albums";

        private readonly IImageMerger _imageMerger;
        private readonly DataDownloader _dataDownloader;
        private readonly ImageRepo _imageRepo;
        private readonly WebClient _webClient;

        public PagedAlbumCollectionGenerator(IImageMerger imageMerger, IImageRepo imageRepo)
        {
            _dataDownloader = new DataDownloader();
            _imageMerger = imageMerger;
            _webClient = new WebClient();
            _imageRepo = imageRepo;
        }

        public List<Album> Generate(int pageNumber, int numberOfObjectsPerPage)
        {
            var imageAndAlbumData = GetImageAndAlbumData();

            var image = _imageRepo.Consume<ImageJson>(imageAndAlbumData["Images"]);
            var album = _imageRepo.Consume<AlbumJson>(imageAndAlbumData["Albums"]);

            var mergedAlbumCollection = _imageMerger.Merge(image, album).ToList();

            var pagedAlbumCollection = Pager.Page(mergedAlbumCollection, pageNumber, numberOfObjectsPerPage).ToList();
            return pagedAlbumCollection;
        }

        private Dictionary<string, string> GetImageAndAlbumData()
        {
            return
                new Dictionary<string, string>
                {
                    {"Images", _dataDownloader.DownloadDataFromUrl(ImagesUrl, _webClient)},
                    {"Albums", _dataDownloader.DownloadDataFromUrl(AlbumsUrl, _webClient)}

                };

        }
    }

    public interface IImageRepo
    {
        IEnumerable<T> Consume<T>(string data);
    }
}