using System.Collections.Generic;
using System.Web.Http.Results;
using ImageMege;
using ImageMege.Controllers;
using ImageMege.Models;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class GetAlbumsControllerShould
    {
        private MergeController _controller;
        private Mock<IPagedAlbumCollectionGenerator> _mockPagedAlbumCollectionGenerator;

        [SetUp]
        public void BeforeEachTest()
        {
            _mockPagedAlbumCollectionGenerator = new Mock<IPagedAlbumCollectionGenerator>();
            _controller = new MergeController(_mockPagedAlbumCollectionGenerator.Object);
        }

        [Test]
        public void ReturnsContentOnValidRequest()
        {
            _mockPagedAlbumCollectionGenerator.Setup(x => x.Generate(1,1)).Returns(new List<Album>{new Album(){AlbumId = 1}});

            var result = _controller.GetAlbums(1, 1);
            var contentResult = result as OkNegotiatedContentResult<List<Album>>;

            contentResult.ShouldNotBeNull();
            contentResult.Content.ShouldNotBeNull();
        }

        [Test]
        public void CallsPagedAlbumCollectionGenerator()
        {
            _controller.GetAlbums(1, 1);

            _mockPagedAlbumCollectionGenerator.Verify(x => x.Generate(1,1), Times.Once);
        }
    }
}
