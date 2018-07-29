using System.Collections.Generic;
using ImageMege;
using ImageMege.Models;
using Moq;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ImageMerge.Specs
{
    [Binding]
    public class MergeImageIntoAlbumSteps
    {
        private List<Album> _result;
        private Mock<IImageMerger> _imageMerger;
        private List<ImageJson> _image;
        private List<AlbumJson> _album;
        private Mock<IImageRepo> _imageRepo;

        public void Setup()
        {
            _imageMerger = new Mock<IImageMerger>();
            _imageRepo = new Mock<IImageRepo>();
        }

        [Given(@"I have the following image")]
        public void GivenIHaveTheFollowingImage(Table table)
        {
            _image = table.CreateInstance<List<ImageJson>>();
        }
        
        [Given(@"the following Album")]
        public void GivenTheFollowingAlbum(Table table)
        {
            _album = table.CreateInstance<List<AlbumJson>>();
        }

        [When(@"When I call the merge operation asking for (.*) page and (.*) results")]
        public void WhenWhenICallTheMergeOperationAskingForPageAndResults(int pageNumber, int numberOfObjects)
        {
            _imageRepo.Setup(x => x.Consume<ImageJson>(It.IsAny<string>())).Returns(_image);
            _imageRepo.Setup(x => x.Consume<AlbumJson>(It.IsAny<string>())).Returns(_album);
            var albumCollectionGenerator = new PagedAlbumCollectionGenerator(_imageMerger.Object, _imageRepo.Object);
            _result = albumCollectionGenerator.Generate(pageNumber, numberOfObjects);
        }
        
        [Then(@"the result should be the following album:")]
        public void ThenTheResultShouldBeTheFollowingAlbum(Table table)
        {
            var reference = table.CreateInstance<Album>();

            foreach (var album in _result)
            {
                album.AlbumId.ShouldBe(reference.AlbumId);
                album.AlbumTitle.ShouldBe(reference.AlbumTitle);
                album.FullImageUrl.ShouldBe(reference.FullImageUrl); 
                album.PhotoId.ShouldBe(reference.PhotoId); 
                album.ThumbnailUrl.ShouldBe(reference.ThumbnailUrl); 
                album.PhotoTitle.ShouldBe(reference.PhotoTitle); 
                album.UserId.ShouldBe(reference.UserId);
            }

        }
    }
}
