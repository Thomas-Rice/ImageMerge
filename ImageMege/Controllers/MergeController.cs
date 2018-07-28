using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ImageMege.Models;

namespace ImageMege.Controllers
{
    public class MergeController : ApiController
    {
        private const string ImagesUrl = "http://jsonplaceholder.typicode.com/photos";
        private const string AlbumsUrl = "http://jsonplaceholder.typicode.com/albums";
        private readonly ImageRepo _imageRepo;
        private readonly DataDownloader _dataDownloader;
        private readonly WebClient _webClient;
        private readonly ImageMerger _imageMerger;


        public MergeController()
        {
            _webClient = new WebClient();
            _imageRepo = new ImageRepo();
            _dataDownloader = new DataDownloader();
            _imageMerger = new ImageMerger();
        }

        [HttpGet]
        public List<Album> GetAlbums(int pageNumber, int numberOfObjectsPerPage)
        {
            var imagedata =  _dataDownloader.DownloadDataFromUrl(ImagesUrl, _webClient);
            var albumdata =  _dataDownloader.DownloadDataFromUrl(AlbumsUrl, _webClient);

            var image = _imageRepo.Consume<ImageJson>(imagedata);
            var album = _imageRepo.Consume<AlbumJson>(albumdata);

            var mergedAlbumCollection = _imageMerger.Merge(image, album).ToList();

            return Pager.Page(mergedAlbumCollection, pageNumber, numberOfObjectsPerPage).ToList();
        }

    }
}
