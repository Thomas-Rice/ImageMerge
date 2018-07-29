using System.Collections.Generic;
using ImageMege;
using ImageMege.Models;
using Shouldly;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace ImageMerge.Specs
{
    [Binding]
    public class MergeImageIntoAlbumSteps
    {
        private List<Album> _result;

        [Given(@"I have the following image")]
        public void GivenIHaveTheFollowingImage(Table table)
        {
            table.CreateInstance<ImageJson>();
        }
        
        [Given(@"the following Album")]
        public void GivenTheFollowingAlbum(Table table)
        {
            table.CreateInstance<AlbumJson>();
        }

        [When(@"When I call the merge operation asking for (.*) page and (.*) results")]
        public void WhenWhenICallTheMergeOperationAskingForPageAndResults(int pageNumber, int numberOfObjects)
        {
            var albumCollectionGenerator = new PagedAlbumCollectionGenerator();
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
