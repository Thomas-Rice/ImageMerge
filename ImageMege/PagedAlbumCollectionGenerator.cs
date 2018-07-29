using System.Collections.Generic;
using System.Linq;
using System.Net;
using ImageMege.Models;

namespace ImageMege
{
    public class PagedAlbumCollectionGenerator
    {
        private const string ImagesUrl = "http://jsonplaceholder.typicode.com/photos";
        private const string AlbumsUrl = "http://jsonplaceholder.typicode.com/albums";
        private readonly ImageMerger _imageMerger;
        private readonly DataDownloader _dataDownloader;
        private readonly ImageRepo _imageRepo;
        private readonly WebClient _webClient;

        public PagedAlbumCollectionGenerator()
        {
            _dataDownloader = new DataDownloader();
            _imageMerger = new ImageMerger();
            _webClient = new WebClient();
            _imageRepo = new ImageRepo();
        }

        public List<Album> Generate(int pageNumber, int numberOfObjectsPerPage)
        {
            var imagedata = _dataDownloader.DownloadDataFromUrl(ImagesUrl, _webClient);
            var albumdata = _dataDownloader.DownloadDataFromUrl(AlbumsUrl, _webClient);

            var image = _imageRepo.Consume<ImageJson>(imagedata);
            var album = _imageRepo.Consume<AlbumJson>(albumdata);

            var mergedAlbumCollection = _imageMerger.Merge(image, album).ToList();

            var pagedAlbumCollection = Pager.Page(mergedAlbumCollection, pageNumber, numberOfObjectsPerPage).ToList();
            return pagedAlbumCollection;
        }
    }
}