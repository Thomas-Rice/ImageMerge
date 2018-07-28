using ImageMege.Controllers;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class GetAlbumsControllerShould
    {
        private MergeController _controller;

        [SetUp]
        public void BeforeEachTest()
        {
            //_mockWebClient = new Mock<IWebClient>();
            _controller = new MergeController();
        }

        [Test]
        public void ReturnsAJsonString()
        {
            var result = _controller.GetAlbums();

            result.ShouldBeOfType<string>();
        }

        //[Test]
        //public void CallsWebClient()
        //{
        //    _controller.GetAlbums();

        //    _mockWebClient.Verify(x=> x.DownloadString(It.IsAny<string>()), Times.Once);
        //}

        //[Test]
        //public void CallImageRepo()
        //{
        //    var result = _controller.GetAlbums();

        //    _mockImageMerger.Verify(x => x.Merge(), Times.Once);

        //}
    }
}
