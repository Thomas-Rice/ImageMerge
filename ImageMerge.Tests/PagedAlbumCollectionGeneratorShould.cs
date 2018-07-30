using System.Collections.Generic;
using System.Linq;
using System.Net;
using ImageMege;
using ImageMege.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class PagedAlbumCollectionGeneratorShould
    {
        private PagedAlbumCollectionGenerator _pagedAlbumCollectionGenerator;
        private Mock<IImageRepo> _imageRepo;
        private Mock<IImageMerger> _imageMerger;
        private Mock<IWebClient> _webClient;

        private List<AlbumJson> _album;
        private List<ImageJson> _image;
        private List<Album> _expectedResultForMultiple;
        private const string ImagesUrl = "http://jsonplaceholder.typicode.com/photos";
        private const string AlbumsUrl = "http://jsonplaceholder.typicode.com/albums";
        private const string _imageData = "[{\"albumId\": 1,\"id\": 1, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"},{\"albumId\": 1,\"id\": 2, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"},{\"albumId\": 1,\"id\": 3, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"}]";
        private const string _albumData = "[{\"id\": 1,\"title\": \"TEST\", \"userId\":1}]";

        [SetUp]
        public void BeforeEachTest()
        {
            _imageRepo = new Mock<IImageRepo>();
            _imageMerger = new Mock<IImageMerger>();
            _webClient = new Mock<IWebClient>();
            _pagedAlbumCollectionGenerator = new PagedAlbumCollectionGenerator(new ImageMerger(), new ImageRepo(), _webClient.Object);

            _image = new List<ImageJson>();
            _album = new List<AlbumJson>();

            _expectedResultForMultiple = new List<Album>
            {

                new Album
                {
                    AlbumId = 1,
                    AlbumTitle = "TEST",
                    FullImageUrl = "TestUrl",
                    PhotoId = 1,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                },
                new Album
                {
                    AlbumId = 1,
                    FullImageUrl = "TestUrl",
                    AlbumTitle = "TEST",
                    PhotoId = 2,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                },
                new Album
                {
                    AlbumId = 1,
                    AlbumTitle = "",
                    FullImageUrl = "TestUrl",
                    PhotoId = 3,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                },
                new Album
                {
                    AlbumId = 1,
                    FullImageUrl = "TestUrl",
                    PhotoId = 4,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                }
            };
        }

        [TestCase(1,1,1)]
        [TestCase(2,1,1)]
        [TestCase(1,2,2)]
        public void ReturnASpecifiedAmountOfObjects(int pageNumber, int numberOfObjectsPerPage, int numberOfResults)
        {
            var result = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            result.Count.ShouldBe(numberOfResults);
        }

        [TestCase(1, 1, 0)]
        [TestCase(2, 1, 1)]
        public void ReturnASingleObjectInCollectionWithCorrectData(int pageNumber, int numberOfObjectsPerPage, int index)
        {
            _webClient.Setup(x => x.DownloadString(ImagesUrl)).Returns(_imageData);
            _webClient.Setup(x => x.DownloadString(AlbumsUrl)).Returns(_albumData);

            var result = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            result.First().AlbumId.ShouldBe(_expectedResultForMultiple[index].AlbumId);
            result.First().AlbumTitle.ShouldBe(_expectedResultForMultiple[index].AlbumTitle);
            result.First().PhotoId.ShouldBe(_expectedResultForMultiple[index].PhotoId);
            result.First().FullImageUrl.ShouldBe(_expectedResultForMultiple[index].FullImageUrl);
            result.First().PhotoTitle.ShouldBe(_expectedResultForMultiple[index].PhotoTitle);
            result.First().ThumbnailUrl.ShouldBe(_expectedResultForMultiple[index].ThumbnailUrl);
            result.First().UserId.ShouldBe(_expectedResultForMultiple[index].UserId);
        }

        [Test]
        public void ReturnNullIfAlbumOrImageAreEmpty()
        {
            _imageRepo.Setup(y => y.Consume<ImageJson>(It.IsAny<string>())).Returns(new List<ImageJson>());
            _imageRepo.Setup(y => y.Consume<AlbumJson>(It.IsAny<string>())).Returns(new List<AlbumJson>());

            var result = _pagedAlbumCollectionGenerator.Generate(1, 1);

            result.ShouldBeNull();
        }
    }
}