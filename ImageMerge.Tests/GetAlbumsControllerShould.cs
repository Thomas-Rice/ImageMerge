using System.Collections.Generic;
using System.Web.Http.Results;
using ImageMege.Controllers;
using ImageMege.Models;
using NUnit.Framework;

namespace ImageMerge.Tests
{
    public class GetAlbumsControllerShould
    {
        private MergeController _controller;

        [SetUp]
        public void BeforeEachTest()
        {
            _controller = new MergeController();
        }

        [Test]
        public void Returns200OnValidRequest()
        {
            var result = _controller.GetAlbums(1,1);


            var contentResult = result as OkNegotiatedContentResult<List<Album>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            //Assert.AreEqual(42, contentResult.Content.Id);
        }
    }
}
