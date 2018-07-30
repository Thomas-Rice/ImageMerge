using System.Collections.Generic;
using ImageMege.Models;

namespace ImageMege.Services
{
    public interface IImageMerger
    {
        IEnumerable<Album> Merge(IEnumerable<ImageJson> imageCollection, IEnumerable<AlbumJson> albumCollection);
    }
}