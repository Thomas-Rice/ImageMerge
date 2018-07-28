using System.Linq;
using ImageMege;
using ImageMege.Models;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class ImageRepoShould
    {
        private ImageRepo _imageRepo;
        private string _imageData;
        private string _albumData;
        private ImageJson _resultImage;
        private AlbumJson _resultAlbum;

        [SetUp]
        public void BeforeEachTest()
        {
            _imageRepo = new ImageRepo();

            _imageData = "[{\"albumId\": 1,\"id\": 1, \"title\":\"T\",\"url\":\"LOL\",\"thumbnailUrl\":\"LOL\"}]";
            _albumData = "[{\"id\": 1,\"title\": \"TEST\", \"userId\":1}]";

            _resultImage = new ImageJson
            {
                albumId = 1,
                id = 1,
                thumbnailUrl = "LOL",
                title = "T",
                url = "LOL"
            };

            _resultAlbum = new AlbumJson
            {
                id = 1,
                title = "TEST",
                userId = 1
            };

        }

        [Test]
        public void ConsumeImageDataToType()
        {
            var result = _imageRepo.Consume<ImageJson>(_imageData);

            result.FirstOrDefault().ShouldBeOfType<ImageJson>();
        }

        [Test]
        public void ConsumeImageData()
        {
            var result = _imageRepo.Consume<ImageJson>(_imageData).FirstOrDefault();


            result.albumId.ShouldBe(_resultImage.albumId);
            result.id.ShouldBe(_resultImage.id);
            result.thumbnailUrl.ShouldBe(_resultImage.thumbnailUrl);
            result.title.ShouldBe(_resultImage.title);
            result.url.ShouldBe(_resultImage.url);
        }

        [Test]
        public void ConsumeAlbumDataToType()
        {
            var result = _imageRepo.Consume<AlbumJson>(_albumData);

            result.FirstOrDefault().ShouldBeOfType<AlbumJson>();
        }

        [Test]
        public void ConsumeAlbumData()
        {
            var result = _imageRepo.Consume<AlbumJson>(_albumData).FirstOrDefault();

            result.id.ShouldBe(_resultAlbum.id);
            result.title.ShouldBe(_resultAlbum.title);
            result.userId.ShouldBe(_resultAlbum.userId);
        }


    }
}
