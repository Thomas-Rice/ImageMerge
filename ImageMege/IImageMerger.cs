using System.Collections.Generic;
using ImageMege.Models;

namespace ImageMege
{
    public interface IImageMerger
    {
        IEnumerable<Album> Merge(IEnumerable<ImageJson> imageCollection, IEnumerable<AlbumJson> albumCollection);
    }
}