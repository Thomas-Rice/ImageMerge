using System.Collections.Generic;
using System.Linq;
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
        private List<AlbumJson> _album;
        private List<ImageJson> _image;
        private List<Album> _expectedResultForMultiple;

        [SetUp]
        public void BeforeEachTest()
        {
            _imageRepo = new Mock<IImageRepo>();
            _imageMerger = new Mock<IImageMerger>();
            _pagedAlbumCollectionGenerator = new PagedAlbumCollectionGenerator(_imageMerger.Object, _imageRepo.Object);

            _image = new List<ImageJson>();
            _album = new List<AlbumJson>();

            _expectedResultForMultiple = new List<Album>
            {

                new Album
                {
                    AlbumId = 1,
                    AlbumTitle = "",
                    FullImageUrl = "TestUrl",
                    PhotoId = 5,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                },
                new Album
                {
                    AlbumId = 1,
                    FullImageUrl = "TestUrl",
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

            _imageRepo.Setup(x => x.Consume<AlbumJson>(It.IsAny<string>())).Returns(_album);
            _imageRepo.Setup(x => x.Consume<ImageJson>(It.IsAny<string>())).Returns(_image);
        }

        [TestCase(1,1,1)]
        [TestCase(2,1,1)]
        [TestCase(1,2,2)]
        public void ReturnASpecifiedAmountOfObjects(int pageNumber, int numberOfObjectsPerPage, int numberOfResults)
        {
            _imageMerger.Setup(y => y.Merge(_image, _album)).Returns(_expectedResultForMultiple);
            var result = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            result.Count.ShouldBe(numberOfResults);
        }

        [TestCase(1, 1, 0)]
        [TestCase(2, 1, 1)]
        public void ReturnASingleObjectInCollectionWithCorrectData(int pageNumber, int numberOfObjectsPerPage, int index)
        {
            _imageMerger.Setup(y => y.Merge(_image, _album)).Returns(_expectedResultForMultiple);
            var result = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            result.First().AlbumId.ShouldBe(_expectedResultForMultiple[index].AlbumId);
            result.First().AlbumTitle.ShouldBe(_expectedResultForMultiple[index].AlbumTitle);
            result.First().PhotoId.ShouldBe(_expectedResultForMultiple[index].PhotoId);
            result.First().FullImageUrl.ShouldBe(_expectedResultForMultiple[index].FullImageUrl);
            result.First().PhotoTitle.ShouldBe(_expectedResultForMultiple[index].PhotoTitle);
            result.First().ThumbnailUrl.ShouldBe(_expectedResultForMultiple[index].ThumbnailUrl);
            result.First().UserId.ShouldBe(_expectedResultForMultiple[index].UserId);
        }
    }
}