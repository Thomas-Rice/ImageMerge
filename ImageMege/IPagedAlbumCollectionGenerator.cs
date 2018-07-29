using System.Collections.Generic;
using ImageMege.Models;

namespace ImageMege
{
    public interface IPagedAlbumCollectionGenerator
    {
        List<Album> Generate(int pageNumber, int numberOfObjectsPerPage);
    }
}