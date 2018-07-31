using System.Collections.Generic;
using ImageMege;
using ImageMege.Models;
using ImageMege.Services;
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
        private IImageMerger _imageMerger;
        private List<ImageJson> _imageCollection;
        private List<AlbumJson> _albumCollection;
        private Mock<IImageRepo> _imageRepo;
        private Mock<IWebClient> _webClient;

        [Given(@"Given Configurations Are SetUp")]
        public void GivenConfigurationsAreSetUp()
        {
            _imageMerger = new ImageMerger();
            _imageRepo = new Mock<IImageRepo>();
            _webClient = new Mock<IWebClient>();

            _imageCollection = new List<ImageJson>();
            _albumCollection = new List<AlbumJson>();
        }

        [Given(@"I have the following image")]
        public void GivenIHaveTheFollowingImage(Table table)
        {
            var image = table.CreateInstance<ImageJson>();
            _imageCollection.Add(image);
        }
        
        [Given(@"the following Album")]
        public void GivenTheFollowingAlbum(Table table)
        {
            var album = table.CreateInstance<AlbumJson>();
            _albumCollection.Add(album);
        }

        [When(@"When I call the merge operation asking for (.*) page and (.*) results")]
        public void WhenWhenICallTheMergeOperationAskingForPageAndResults(int pageNumber, int numberOfObjects)
        {
            //_webClient.Setup(x => x.DownloadString("TestImageString")).Returns(_imageData);
            //_webClient.Setup(x => x.DownloadString(AlbumsUrl)).Returns(_albumData);

            _imageRepo.Setup(x => x.Consume<ImageJson>(It.IsAny<string>())).Returns(_imageCollection);
            _imageRepo.Setup(x => x.Consume<AlbumJson>(It.IsAny<string>())).Returns(_albumCollection);

            var albumCollectionGenerator = new PagedAlbumCollectionGenerator(_imageMerger, _imageRepo.Object, _webClient.Object);
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
