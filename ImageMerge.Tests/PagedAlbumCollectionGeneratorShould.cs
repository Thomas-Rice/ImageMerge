using System.Collections.Generic;
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
        private List<ImageJson> _multipleImages;
        private List<Album> _expectedResultForMultiple;

        [SetUp]
        public void BeforeEachTest()
        {
            _imageRepo = new Mock<IImageRepo>();
            _imageMerger = new Mock<IImageMerger>();
            _pagedAlbumCollectionGenerator = new PagedAlbumCollectionGenerator(_imageMerger.Object, _imageRepo.Object);

            _multipleImages = new List<ImageJson>
            {
                new ImageJson
                {
                    albumId = 1,
                    id = 1,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                },
                new ImageJson
                {
                    albumId = 1,
                    id = 2,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                },
                new ImageJson
                {
                    albumId = 2,
                    id = 1,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                }
            };

            _album = new List<AlbumJson>
            {
                new AlbumJson
                {
                    id = 1,
                    title = "TestAlbumTitle",
                    userId = 1
                }
            };

            _expectedResultForMultiple = new List<Album>
            {

                new Album
                {
                    AlbumId = 1,
                    AlbumTitle = "",
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
                    PhotoId = 2,
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
            _imageRepo.Setup(x => x.Consume<AlbumJson>(It.IsAny<string>())).Returns(_album);
            _imageRepo.Setup(x => x.Consume<ImageJson>(It.IsAny<string>())).Returns(_multipleImages);
            _imageMerger.Setup(y => y.Merge(_multipleImages, _album)).Returns(_expectedResultForMultiple);
            var result = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            result.Count.ShouldBe(numberOfResults);
        }


    }
}