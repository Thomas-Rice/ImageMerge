using System.Collections.Generic;
using System.Linq;
using ImageMege;
using ImageMege.Models;
using ImageMege.Services;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class PagedAlbumCollectionGeneratorShould
    {
        private PagedAlbumCollectionGenerator _pagedAlbumCollectionGenerator;
        private Mock<IWebClient> _webClient;

        private List<Album> _expectedResultForMultiple;
        private const string ImagesUrl = "http://jsonplaceholder.typicode.com/photos";
        private const string AlbumsUrl = "http://jsonplaceholder.typicode.com/albums";
        private const string _imageData = "[{\"albumId\": 1,\"id\": 1, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"},{\"albumId\": 1,\"id\": 2, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"},{\"albumId\": 1,\"id\": 3, \"title\":\"TestTitle\",\"url\":\"TestUrl\",\"thumbnailUrl\":\"TestURl\"}]";
        private const string _albumData = "[{\"id\": 1,\"title\": \"TEST\", \"userId\":1}]";

        [SetUp]
        public void BeforeEachTest()
        {
            _webClient = new Mock<IWebClient>();
            _pagedAlbumCollectionGenerator = new PagedAlbumCollectionGenerator(new ImageMerger(), new ImageRepo(), _webClient.Object);

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
            _webClient.Setup(x => x.DownloadString(ImagesUrl)).Returns(_imageData);
            _webClient.Setup(x => x.DownloadString(AlbumsUrl)).Returns(_albumData);

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
            _webClient.Setup(x => x.DownloadString(ImagesUrl)).Returns("");
            _webClient.Setup(x => x.DownloadString(AlbumsUrl)).Returns("");

            var result = _pagedAlbumCollectionGenerator.Generate(1, 1);

            result.ShouldBeNull();
        }
    }
}