using ImageMege.Models;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ImageMerge.Specs
{
    [Binding]
    public class MergeImageIntoAlbumSteps
    {
        private Album _result;
        private ImageJson _image;
        private AlbumJson _album;

        [Given(@"I have the following image")]
        public void GivenIHaveTheFollowingImage(Table table)
        {
            _image = table.CreateInstance<ImageJson>();
        }
        
        [Given(@"the following Album")]
        public void GivenTheFollowingAlbum(Table table)
        {
            _album = table.CreateInstance<AlbumJson>();
        }
        
        [When(@"When I call the merge operation")]
        public void WhenWhenICallTheMergeOperation()
        {
            //var imageMerge = new ImageMege();
            //_result = ImageMege.CreateAlbum(_image, _album);
            return;
        }
        
        [Then(@"the result should be the following album:")]
        public void ThenTheResultShouldBeTheFollowingAlbum(Table table)
        {
            var reference = table.CreateInstance<Album>();

            _result.FullImageUrl.ShouldBe(reference.FullImageUrl); 
            _result.PhotoId.ShouldBe(reference.PhotoId); 
            _result.ThumbnailUrl.ShouldBe(reference.ThumbnailUrl); 
            _result.PhotoTitle.ShouldBe(reference.PhotoTitle); 
            _result.UserId.ShouldBe(reference.UserId); 
        }
    }
}
