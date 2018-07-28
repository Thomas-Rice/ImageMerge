using System.Collections.Generic;
using System.Linq;
using ImageMege.Models;

namespace ImageMege
{
    public class ImageMerger
    {
        public IEnumerable<Album> Merge(IEnumerable<ImageJson> imageCollection, IEnumerable<AlbumJson> albumCollection)
        {
            var albums =
                from album in albumCollection
                join photo in imageCollection on album.id equals photo.albumId
                select new Album
                {
                    AlbumId = album.id,
                    AlbumTitle = album.title,
                    UserId = album.userId,
                    PhotoId = photo.id,
                    PhotoTitle = photo.title,
                    ThumbnailUrl = photo.thumbnailUrl,
                    FullImageUrl = photo.url
                };

            return albums;
        }
    }
}