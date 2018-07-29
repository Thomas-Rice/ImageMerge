using System.Collections.Generic;
using System.Linq;
using ImageMege;
using ImageMege.Models;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    public class PagerShould
    {
        private List<Album> _testCollection;

        [SetUp]
        public void Setup()
        {
            _testCollection = new List<Album>();

            for (var i = 0; i < 30; i++)
            {
                _testCollection.Add(new Album {PhotoId = i});
            }
        }

        [TestCase(5)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(28)]
        public void ReturnASpecifiedNumberOfResults(int numberOfResults)
        {
            var result = Pager.Page(_testCollection, 1, numberOfResults);

            result.Count().ShouldBe(numberOfResults);
        }

        [TestCase(5,1,0,4)]
        [TestCase(5,2,5,9)]
        [TestCase(5,3,10,14)]
        [TestCase(15,2,15,29)]
        public void ReturnTheCorrectPhotoIdsAccoringToPageNumber(int numberOfResultsPerPage, int pageNumber, int startingNumber, int endingNumber)
        {
            var result = Pager.Page(_testCollection, pageNumber, numberOfResultsPerPage).ToList();

            result.FirstOrDefault()?.PhotoId.ShouldBe(startingNumber);
            result.Last().PhotoId.ShouldBe(endingNumber);
        }

        [TestCase(40, 1, 0, 29)]
        public void SpecifyMoreResultsThanAvailableReturnsAllResults(int numberOfResultsPerPage, int pageNumber, int startingNumber, int endingNumber)
        {
            var result = Pager.Page(_testCollection, pageNumber, numberOfResultsPerPage).ToList();

            result.FirstOrDefault()?.PhotoId.ShouldBe(startingNumber);
            result.Last().PhotoId.ShouldBe(endingNumber);
        }
    }
}
