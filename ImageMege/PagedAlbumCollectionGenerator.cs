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
        private readonly IImageRepo _imageRepo;
        private readonly IWebClient _webClient;

        public PagedAlbumCollectionGenerator(IImageMerger imageMerger, IImageRepo imageRepo, IWebClient webClient)
        {
            _imageMerger = imageMerger;
            _imageRepo = imageRepo;
            _webClient = webClient;
        }

        public List<Album> Generate(int pageNumber, int numberOfObjectsPerPage)
        {
            var imageAndAlbumData = GetImageAndAlbumData();

            var image = _imageRepo.Consume<ImageJson>(imageAndAlbumData["Images"]);
            var album = _imageRepo.Consume<AlbumJson>(imageAndAlbumData["Albums"]);

            if (!image.Any() || !album.Any()) return null;

            var mergedAlbumCollection = _imageMerger.Merge(image, album).ToList();
            return Pager.Page(mergedAlbumCollection, pageNumber, numberOfObjectsPerPage).ToList();
        }

        private Dictionary<string, string> GetImageAndAlbumData()
        {
            return
                new Dictionary<string, string>
                {
                    {"Images", _webClient.DownloadString(ImagesUrl)},
                    {"Albums", _webClient.DownloadString(AlbumsUrl)}
                };

        }
    }
}