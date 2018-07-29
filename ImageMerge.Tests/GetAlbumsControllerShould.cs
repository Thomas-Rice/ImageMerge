using System.Collections.Generic;
using System.Web.Http.Results;
using ImageMege;
using ImageMege.Controllers;
using ImageMege.Models;
using Moq;
using NUnit.Framework;

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
        public void Returns200OnValidRequest()
        {
            var result = _controller.GetAlbums(1,1);

            var contentResult = result as OkNegotiatedContentResult<List<Album>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }
    }
}
