using System.Collections.Generic;
using System.Linq;
using ImageMege;
using ImageMege.Models;
using NUnit.Framework;
using Shouldly;

namespace ImageMerge.Tests
{
    [TestFixture]
    public class ImageMergerShould
    {
        private List<ImageJson> _image;
        private List<AlbumJson> _album;
        private ImageMerger _imageMerger;
        private Album _expectedResultForSingle;
        private List<ImageJson> _multipleImages;
        private List<Album> _expectedResultForMultiple;
        private List<AlbumJson> _album2;

        [SetUp]
        public void SetUp()
        {
            _imageMerger = new ImageMerger();

            _image = new List<ImageJson>
            {
                new ImageJson
                {
                    albumId = 1,
                    id = 1,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                }
            };

            _multipleImages = new List<ImageJson>
            {
                new ImageJson
                {
                    albumId = 1,
                    id = 1,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                },
                new ImageJson
                {
                    albumId = 1,
                    id = 2,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                },
                new ImageJson
                {
                    albumId = 2,
                    id = 1,
                    thumbnailUrl = "TestURl",
                    title = "TestTitle",
                    url = "TestUrl"
                }
            };

            _album = new List<AlbumJson>
            {
                new AlbumJson
                {
                    id = 1,
                    title = "TestAlbumTitle",
                    userId = 1
                }
                
            };

            _album2 = new List<AlbumJson>
            {
                new AlbumJson
                {
                    id = 3,
                    title = "TestAlbumTitle2",
                    userId = 1
                }

            };

            _expectedResultForSingle = new Album
            {
                AlbumId = 1,
                AlbumTitle = "TestAlbumTitle",
                FullImageUrl = "TestUrl",
                PhotoId = 1,
                ThumbnailUrl = "TestURl",
                PhotoTitle = "TestTitle",
                UserId = 1
            };

            _expectedResultForMultiple = new List<Album>
            {
                
                new Album
                {
                    AlbumId = 1,
                    AlbumTitle = "",
                    FullImageUrl = "TestUrl",
                    PhotoId = 1,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                },
                new Album
                {
                    AlbumId = 1,
                    FullImageUrl = "TestUrl",
                    PhotoId = 2,
                    ThumbnailUrl = "TestURl",
                    PhotoTitle = "TestTitle",
                    UserId = 1
                }
            };

        }

        [Test]
        public void ReturnAlbumCollection()
        {
            var result = _imageMerger.Merge(_image, _album);

            result.ShouldBeOfType<List<Album>>();
        }

        [Test]
        public void MergeAllImagesAndAlbumData()
        {
            var result = _imageMerger.Merge(_image, _album);

            foreach (var album in result)
            {
                album.AlbumId.ShouldBe(_expectedResultForSingle.AlbumId);
                album.FullImageUrl.ShouldBe(_expectedResultForSingle.FullImageUrl);
                album.PhotoId.ShouldBe(_expectedResultForSingle.PhotoId);
                album.ThumbnailUrl.ShouldBe(_expectedResultForSingle.ThumbnailUrl);
                album.PhotoTitle.ShouldBe(_expectedResultForSingle.PhotoTitle);
                album.UserId.ShouldBe(_expectedResultForSingle.UserId);
            }
        }

        [Test]
        public void MergeMultiplePhotosAndIgnoreIfAlbumNotPresent()
        {
            var result = _imageMerger.Merge(_multipleImages, _album).ToList();

            for (var i = 0; i < result.Count; i++)
            {
                result[i].AlbumId.ShouldBe(_expectedResultForMultiple[i].AlbumId);
                result[i].FullImageUrl.ShouldBe(_expectedResultForMultiple[i].FullImageUrl);
                result[i].PhotoId.ShouldBe(_expectedResultForMultiple[i].PhotoId);
                result[i].ThumbnailUrl.ShouldBe(_expectedResultForMultiple[i].ThumbnailUrl);
                result[i].PhotoTitle.ShouldBe(_expectedResultForMultiple[i].PhotoTitle);
                result[i].UserId.ShouldBe(_expectedResultForMultiple[i].UserId);
            }
        }

        [Test]
        public void MergeNoneIfAlbumNotPresent()
        {
            var result = _imageMerger.Merge(_multipleImages, _album2).ToList();

            result.Count.ShouldBe(0);
        }


    }
}
